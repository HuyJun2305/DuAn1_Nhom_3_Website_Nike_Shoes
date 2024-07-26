using AppView.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class SanPhamController : Controller
    {
        AppDbContext context;
        public SanPhamController()
        {
            context = new AppDbContext();
        }
        // GET: SanPhamController
        public async Task<IActionResult> Index()
        {
            // Lấy thông tin sản phẩm cùng với thông tin danh mục
            var data = await context.sanPhams
                                    .Include(sp => sp.DanhMucSanPham) // Bao gồm thông tin danh mục sản phẩm
                                    .ToListAsync();

            return View(data);
        }


        // GET: SanPhamController/Details/5
        public ActionResult Details(Guid id)
        {
            var data = context.sanPhams.Find(id);
            return View(data);
        }

        // GET: SanPhamController/Create
        public async Task<IActionResult> Create()
        {
            var dmsps = await context.danhMucSanPhams.ToListAsync();
            ViewBag.danhMucSanPhams = dmsps;
            return View();
        }

        // POST: SanPhamController/Create
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sp, IFormFile imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile != null && imgFile.Length > 0)
                {
                    // Đặt tên tệp để tránh xung đột
                    var fileName = Path.GetFileName(imgFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                    // Lưu tệp hình ảnh vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(stream);
                    }

                    // Lưu tên tệp vào mô hình SanPham
                    sp.ImgFile = $"/img/{fileName}";
                }
                else
                {
                    // Nếu không có tệp hình ảnh, đặt giá trị mặc định hoặc bỏ qua
                    sp.ImgFile = null; // hoặc giá trị mặc định khác nếu cần
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                context.sanPhams.Add(sp);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, lấy danh mục sản phẩm và trả về view
            ViewBag.danhMucSanPhams = await context.danhMucSanPhams.ToListAsync();
            return View(sp);
        }

        // GET: SanPhamController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var data = context.sanPhams.Find(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, SanPham sp, IFormFile imgFile)
        {
            try
            {
                // Tìm sản phẩm cần cập nhật
                var editsp = await context.sanPhams.FindAsync(id);
                if (editsp == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin sản phẩm
                editsp.Ten = sp.Ten;
                editsp.Gia = sp.Gia;
                editsp.SoLuong = sp.SoLuong;
                editsp.MauSac = sp.MauSac;
                editsp.Size = sp.Size;
                editsp.TrangThai = sp.TrangThai;

                if (imgFile != null && imgFile.Length > 0)
                {
                    // Xóa hình ảnh cũ nếu cần thiết (tùy vào logic của bạn)
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", editsp.ImgFile);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // Lưu hình ảnh mới
                    var fileName = Path.GetFileName(imgFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(stream);
                    }

                    // Cập nhật đường dẫn hình ảnh trong cơ sở dữ liệu
                    editsp.ImgFile = fileName;
                }

                // Cập nhật sản phẩm trong cơ sở dữ liệu
                context.Update(editsp);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và có thể ghi log hoặc trả về thông báo lỗi chi tiết
                return BadRequest($"Có lỗi xảy ra: {ex.Message}");
            }
        }


        // GET: SanPhamController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var deletesp = context.sanPhams.Find(id);
            context.Remove(deletesp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult AddToCart(Guid id, int amount) // id này là id của sản phẩm
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            var username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                return Content("Đã đăng nhập đâu mà đòi thêm thắt cái gì?");
            }

            var idGH = Guid.Parse(username);

            // Kiểm tra xem giỏ hàng của người dùng có tồn tại không
            var userCart = context.gioHangs.Find(idGH);
            if (userCart == null)
            {
                // Tạo giỏ hàng mới nếu chưa tồn tại
                userCart = new GioHang
                {
                    Id = idGH
                };
                context.gioHangs.Add(userCart);
                context.SaveChanges();
            }

            // Kiểm tra xem sản phẩm có tồn tại không
            var sanPham = context.sanPhams.Find(id);
            if (sanPham == null)
            {
                return Content("Sản phẩm không tồn tại.");
            }

            // Tìm sản phẩm trong giỏ hàng
            var existingItem = context.gioHangChiTiets
                .FirstOrDefault(p => p.IdGH == idGH && p.IdSP == id);

            if (existingItem == null)
            {
                // Thêm mới chi tiết giỏ hàng
                var ghct = new GioHangChiTiet
                {
                    Id = Guid.NewGuid(),
                    IdSP = id,
                    IdGH = idGH,
                    SoLuong = amount,
                };
                context.gioHangChiTiets.Add(ghct);
            }
            else
            {
                // Cập nhật số lượng sản phẩm trong giỏ hàng
                existingItem.SoLuong += amount;
                context.gioHangChiTiets.Update(existingItem);
            }
            sanPham.SoLuong -= amount;
            context.sanPhams.Update(sanPham);

            context.SaveChanges();
            return RedirectToAction("Index");

        }

            //public IActionResult AddToCart(Guid id, int amount)
            //{
            //    var username = HttpContext.Session.GetString("username");
            //    if (string.IsNullOrEmpty(username))
            //    {
            //        return Content("Đã đăng nhập đâu anh bạn!");
            //    }

            //    var user = context.khachHangs.FirstOrDefault(u => u.Username == username);
            //    if (user == null)
            //    {
            //        return Content("Người dùng không tồn tại!");
            //    }

            //    var userId = user.Id; // Assuming 'Id' is the GUID for the user

            //    // Kiểm tra xem IdGH có tồn tại trong bảng gioHangs không
            //    var userCartExists = context.gioHangs.Any(g => g.Id == userId);
            //    if (!userCartExists)
            //    {
            //        return Content("Giỏ hàng không tồn tại!");
            //    }

            //    var userCart = context.gioHangChiTiets.FirstOrDefault(x => x.IdGH == userId && x.IdSP == id);

            //    if (userCart == null)
            //    {
            //        var newCartItem = new GioHangChiTiet
            //        {
            //            Id = Guid.NewGuid(),
            //            IdSP = id,
            //            IdGH = userId,
            //            SoLuong = amount,
            //            Username = username // Set the Username field if needed
            //        };
            //        context.gioHangChiTiets.Add(newCartItem);
            //    }
            //    else
            //    {
            //        userCart.SoLuong += amount;
            //        context.gioHangChiTiets.Update(userCart);
            //    }

            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}



            //public IActionResult AddToCart(Guid id, int amount) // id ở đây là id sản phẩm và amount: số lượng sp
            //{
            //    //check xem đã đăng nhập chưa
            //    var login = HttpContext.Session.GetString("username");
            //    if (login == null)
            //    {
            //        return Content("Đã đăng nhập đâu anh bạn!");
            //    }
            //    else
            //    {
            //        // lấy tất cả sản phẩm có trong giỏ hàng user vừa đăng nhập
            //        var userCart = context.gioHangChiTiets.Where(x => x.IdGH == Guid.Parse(login)).ToList();
            //        bool checkSelected = false;
            //        Guid idGHCT = Guid.Empty;
            //        foreach (var item in userCart)
            //        {
            //            if (item.IdSP == id)
            //            {
            //                // nếu id sp trong giỏ hàng đã trùng với id được chọn
            //                checkSelected = true;
            //                idGHCT = item.Id; // lấy Id GHCT để tý nữa update
            //                break;
            //            }
            //        }
            //        if (!checkSelected) // nếu sp chưa được chọn
            //        {
            //            // tạo mới 1 GHCT ứng với sản phẩm
            //            GioHangChiTiet ghct = new GioHangChiTiet()
            //            {
            //                Id = Guid.NewGuid(),
            //                IdSP = id,
            //                IdGH = Guid.Parse(login),
            //                SoLuong = amount
            //            };
            //            context.gioHangChiTiets.Add(ghct);
            //            context.SaveChanges();
            //            return RedirectToAction("Index");
            //        }
            //        else // nếu sản phẩm được chọn
            //        {
            //            var updateGHCT = context.gioHangChiTiets.Find(idGHCT);
            //            updateGHCT.SoLuong = updateGHCT.SoLuong + amount;
            //            context.gioHangChiTiets.Update(updateGHCT);
            //            context.SaveChanges(true);
            //            return RedirectToAction("Index");

            //        }
            //    }

            //}
        }
    }

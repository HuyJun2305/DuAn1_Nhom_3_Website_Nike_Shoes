using AppView.Models;
using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly AppDbContext context; // Đảm bảo bạn đã thêm ApplicationDbContext vào DI container

        public SanPhamController(AppDbContext _context)
        {
            context = _context;
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

        [HttpPost]
        // POST: SanPhamController/Create
        public async Task<IActionResult> Create(SanPham sp, IFormFile imgFile)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra lỗi model
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                // Ghi log hoặc xem xét các lỗi này
            }

            if (imgFile != null && imgFile.Length > 0)
            {
                var fileName = Path.GetFileName(imgFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(stream);
                    }
                    sp.ImgFile = $"/img/{fileName}";
                }
                catch (Exception ex)    
                {
                    ModelState.AddModelError("", $"Không thể lưu tệp hình ảnh: {ex.Message}");
                    ViewBag.danhMucSanPhams = await context.danhMucSanPhams.ToListAsync();
                    return View(sp);
                }
            }
            sp.TrangThai = sp.SoLuong > 0;
            try
            {
                context.sanPhams.Add(sp);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Không thể thêm sản phẩm: {ex.Message}");
            }

            ViewBag.danhMucSanPhams = await context.danhMucSanPhams.ToListAsync();
            return View(sp);
        }

        // GET: SanPhamController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var dmsps = await context.danhMucSanPhams.ToListAsync();
            ViewBag.danhMucSanPhams = dmsps;

            var data =  context.sanPhams.Find(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, SanPham sp, IFormFile imgFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.danhMucSanPhams = await context.danhMucSanPhams.ToListAsync();
                return View(sp);
            }

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
                editsp.Size = sp.Size;

                // Cập nhật trạng thái dựa trên số lượng
                editsp.TrangThai = sp.SoLuong > 0 ? true : false;

                if (imgFile != null && imgFile.Length > 0)
                {
                    // Lưu hình ảnh mới
                    var fileName = Path.GetFileName(imgFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(stream);
                    }

                    // Cập nhật đường dẫn hình ảnh trong cơ sở dữ liệu
                    editsp.ImgFile = $"/img/{fileName}";
                }

                // Cập nhật sản phẩm trong cơ sở dữ liệu
                context.Update(editsp);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và có thể ghi log hoặc trả về thông báo lỗi chi tiết
                ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
            }

            ViewBag.danhMucSanPhams = await context.danhMucSanPhams.ToListAsync();
            return View(sp);
        }
 



        // GET: SanPhamController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var deletesp = context.sanPhams.Find(id);
            context.Remove(deletesp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult AddToCart(Guid id, int amount)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            var username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Login", "User");
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
                TempData["Error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction("Index"); // Chuyển hướng đến trang sản phẩm hoặc trang danh mục
            }

            // Kiểm tra số lượng sản phẩm
            if (sanPham.SoLuong < amount)
            {
                TempData["Error"] = "Số lượng sản phẩm không đủ để thêm vào giỏ hàng.";
                return RedirectToAction("Index"); // Chuyển hướng đến trang sản phẩm hoặc trang danh mục
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

            // Cập nhật số lượng sản phẩm trong kho
            sanPham.SoLuong -= amount;

            // Cập nhật trạng thái của sản phẩm
            sanPham.TrangThai = sanPham.SoLuong > 0 ? true : false;
            context.sanPhams.Update(sanPham);

            context.SaveChanges();

            TempData["Success"] = "Sản phẩm đã được thêm vào giỏ hàng thành công.";
            return RedirectToAction("Index"); // Chuyển hướng đến trang sản phẩm hoặc trang danh mục
        }


        // Phương thức tìm kiếm và lọc sản phẩm
        public async Task<IActionResult> TimKiem(string searchTerm, string categoryName, decimal? minPrice, decimal? maxPrice, int? size)
        {
            var query = context.sanPhams.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(sp => sp.Ten.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(sp => sp.DanhMucSanPham.TenDM.Contains(categoryName));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(sp => sp.Gia >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(sp => sp.Gia <= maxPrice.Value);
            }

            if (size.HasValue)
            {
                query = query.Where(sp => sp.Size == size.Value);
            }

            var products = await query.Include(sp => sp.DanhMucSanPham).ToListAsync();

            return View("Index", products);
        }



    }
}

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
        public ActionResult Index()
        {
            var data = context.sanPhams.ToList();
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
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(stream);
                sp.ImgFile = imgFile.FileName;

                context.sanPhams.Add(sp);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
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
        public ActionResult Edit(SanPham sp, IFormFile imgFile) 
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(stream);
                sp.ImgFile = imgFile.FileName;

                var editsp = context.sanPhams.Find(sp.Id);
                editsp.Ten = sp.Ten;
                editsp.ImgFile = sp.ImgFile;
                editsp.Gia = sp.Gia;
                editsp.SoLuong = sp.SoLuong;
                editsp.MauSac = sp.MauSac;
                editsp.Size = sp.Size;
                editsp.Ten = sp.Ten;
                editsp.TrangThai = sp.TrangThai;
                context.Update(editsp);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
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

        public IActionResult AddToCart(Guid id, int quantity)
        {
            // Kiểm tra xem có đang đăng nhập ko, nếu ko thì bắt đăng nhập
            var check = HttpContext.Session.GetString("username");
            if (String.IsNullOrEmpty(check))
            {
                return RedirectToAction("Login", "User"); // chuyển hướng về trang login
            }
            else
            {
                // Lấy ra từ sanh sách CartDetail của user đang đăng nhập xem có sản phẩm nào trùng id ko?
                var cartItem = context.gioHangChiTiets.FirstOrDefault(p => p.Username == check && p.IdSP == id);
                // Nếu sản phẩm chưa tồn tại <=> cartItem có giá trị null => Tạo ra 1 cart Details với username là tai
                // khoản đang đăng nhập và idProduct là sản phẩm được chọn và số lượng được chọn
                if (cartItem == null)
                {
                    GioHangChiTiet details = new GioHangChiTiet() // Tạo mới 1 cart Details
                    {
                        Id = Guid.NewGuid(),
                        IdSP = id,
                        Username = check,
                        SoLuong = quantity,
                    };
                    context.gioHangChiTiets.Add(details); context.SaveChanges();
                }
                else
                {
                    cartItem.SoLuong = cartItem.SoLuong + quantity; // Chưa check gì trong kho đâu nhé =)))))
                    context.gioHangChiTiets.Update(cartItem); context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "SanPham");
        }



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

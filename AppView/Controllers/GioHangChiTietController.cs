using AppView.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class GioHangChiTietController : Controller
    {
        AppDbContext context;
        public GioHangChiTietController()
        {
            context = new AppDbContext();
        }
        public async Task<IActionResult> Index()
        {
            // Lấy dữ liệu giỏ hàng cùng với thông tin sản phẩm
            var username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                // Chuyển hướng đến trang đăng nhập và thông báo lỗi
                TempData["Message"] = "Bạn chưa đăng nhập. Vui lòng đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Login", "User");
            }

            // Lấy tất cả các chi tiết giỏ hàng của người dùng đã đăng nhập
            var userId = Guid.Parse(username);
            var data = await context.gioHangChiTiets
                                    .Where(ghct => ghct.IdGH == userId) // Lọc theo IdGH của người dùng
                                    .Include(ghct => ghct.SanPham) // Bao gồm thông tin sản phẩm
                                    .ToListAsync();

            return View(data);
        }
    
        //public ActionResult Index()
        //{
        //    var alldata = context.gioHangChiTiets.ToList();
        //    return View(alldata);
        //}

        // GET: GioHangChiTietController/Details/5
        public ActionResult Details(Guid id)
        {

            return View();
        }

        // GET: GioHangChiTietController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GioHangChiTietController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHangChiTietController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GioHangChiTietController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHangChiTietController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var deleteGH = context.gioHangChiTiets.Find(id);
            context.gioHangChiTiets.Remove(deleteGH);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

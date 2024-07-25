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
        public ActionResult Index()
        {
            var data = context.gioHangChiTiets.Where(x => x.IdGH == Guid.Parse(HttpContext.Session.GetString("username"))).ToList();
            return View(data);
        }

        // GET: GioHangChiTietController/Details/5
        public ActionResult Details(int id)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GioHangChiTietController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}

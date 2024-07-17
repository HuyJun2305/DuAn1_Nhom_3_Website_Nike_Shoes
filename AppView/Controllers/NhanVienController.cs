using AppView.AllRepository;
using AppView.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class NhanVienController : Controller
    {
        AppDbContext _context;
        public NhanVienController()
        {
            _context = new AppDbContext();
        }


        // GET: UserController
        public ActionResult Index()
        {
            var data = _context.NhanViens.ToList();
            return View(data);
        }

        // GET: UserController/Details/5
        public ActionResult Details(Guid id)
        {
            var data = _context.NhanViens.Find(id);
            return View(data);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        public ActionResult Create(NhanVien nv, IFormFile imgFile)
        {

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(stream);
                nv.ImgUrl = imgFile.FileName;
                _context.NhanViens.Add(nv);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
        }   

        // GET: UserController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var data = _context.NhanViens.Find(id);
            return View(data);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        public ActionResult Edit(NhanVien nv, IFormFile imgFile)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(stream);
                nv.ImgUrl = imgFile.FileName;


                var editNV = _context.NhanViens.Find(nv.Id);
                editNV.Ten = nv.Ten;
                editNV.ImgUrl = nv.ImgUrl;
                editNV.SDT = nv.SDT;
                editNV.Email = nv.Email;
                editNV.Roles = nv.Roles;
                editNV.TrangThai = nv.TrangThai;
                _context.Update(editNV);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: UserController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: UserController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

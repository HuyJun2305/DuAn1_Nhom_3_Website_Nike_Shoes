using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class DanhMucSanPhamController : Controller
    {
        AppDbContext _context;
        public DanhMucSanPhamController()
        {
            _context = new AppDbContext();
        }
        // GET: DanhMucSanPhamController
        public async Task<IActionResult> Index()
        {
            var danhMucSanPhams = await _context.danhMucSanPhams
                .Select(dm => new DanhMucSanPhamViewModel
                {
                    Id = dm.Id,
                    TenDM = dm.TenDM,
                    ImgUrl = dm.ImgUrl,
                    TrangThai = dm.TrangThai,
                    TongSoLuongSanPham = _context.sanPhams
                        .Where(sp => sp.IdDMSP == dm.Id)
                        .Sum(sp => sp.SoLuong)
                }).ToListAsync();

            return View(danhMucSanPhams);
        }


        // GET: DanhMucSanPhamController/Details/5
        public ActionResult Details(Guid id)
        {
            var data = _context.danhMucSanPhams.Find(id);
            return View(data);
        }

        // GET: DanhMucSanPhamController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DanhMucSanPhamController/Create
        [HttpPost]
        public ActionResult Create(DanhMucSanPham dm, IFormFile imgFile)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
            var stream = new FileStream(path, FileMode.Create);
            imgFile.CopyTo(stream);
            dm.ImgUrl = imgFile.FileName;
            _context.danhMucSanPhams.Add(dm);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: DanhMucSanPhamController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var data = _context.danhMucSanPhams.Find(id);
            return View(data);
        }

        // POST: DanhMucSanPhamController/Edit/5
        [HttpPost]
        public ActionResult Edit(DanhMucSanPham dm, IFormFile imgFile)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(stream);
                dm.ImgUrl = imgFile.FileName;


                var editNV = _context.danhMucSanPhams.Find(dm.Id);
                editNV.ImgUrl = dm.ImgUrl;
                editNV.TenDM = dm.TenDM;
                editNV.TrangThai = dm.TrangThai;
                _context.Update(editNV);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return BadRequest();
            }
        }

    }
}

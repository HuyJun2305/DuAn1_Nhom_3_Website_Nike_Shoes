using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng với vai trò "Admin" truy cập
    public class DanhMucSanPhamController : Controller
    {
        ApplicationDbContext _context;
        public DanhMucSanPhamController(ApplicationDbContext context)
        {
            _context = context;
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
        public ActionResult Edit(DanhMucSanPham dm)
        {
            // Kiểm tra tính hợp lệ của dữ liệu mẫu
            if (!ModelState.IsValid)
            {
                return View(dm); // Trả về view với dữ liệu nhập vào nếu không hợp lệ
            }

            try
            {
                // Tìm đối tượng cần cập nhật
                var editNV = _context.danhMucSanPhams.Find(dm.Id);
                if (editNV == null)
                {
                    return NotFound(); // Nếu không tìm thấy đối tượng
                }

                // Cập nhật các thuộc tính của đối tượng
                editNV.TenDM = dm.TenDM;
                editNV.TrangThai = dm.TrangThai;

                // Cập nhật cơ sở dữ liệu
                _context.Update(editNV);
                _context.SaveChanges();

                // Chuyển hướng đến trang Index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và trả về thông báo lỗi
                // LogError(ex); // Nếu bạn có phương thức ghi log
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật danh mục sản phẩm.");
            }
        }



    }
}

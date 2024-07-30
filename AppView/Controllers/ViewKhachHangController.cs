using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class ViewKhachHangController : Controller
    {
        private readonly AppDbContext _context;

        public ViewKhachHangController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ProductList(int page = 1)
        {
            int pageSize = 9; // Số sản phẩm mỗi trang

            var products = await _context.sanPhams
                .Where(p => p.TrangThai) // Lọc các sản phẩm còn hàng
                .OrderBy(p => p.Ten) // Sắp xếp theo tên sản phẩm
                .Skip((page - 1) * pageSize) // Bỏ qua các sản phẩm của các trang trước
                .Take(pageSize) // Lấy số sản phẩm cho trang hiện tại
                .Select(p => new SanPhamViewModel
                {
                    Id = p.Id,
                    Ten = p.Ten,
                    Gia = p.Gia,
                    SoLuong = p.SoLuong,
                    ImgFile = p.ImgFile
                })
                .ToListAsync();

            var totalProducts = await _context.sanPhams.CountAsync(p => p.TrangThai); // Tổng số sản phẩm

            var viewModel = new SanPhamListViewModel
            {
                Products = products,
                Categories = await _context.danhMucSanPhams
                    .Select(c => new DanhMucSanPhamViewModel
                    {
                        Id = c.Id,
                        TenDM = c.TenDM
                    })
                    .ToListAsync(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize)
            };

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult AddToCart(Guid id, int quantity)
        {
            var username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Login", "User");
            }

            var idGH = Guid.Parse(username);
            var userCart = _context.gioHangs.Find(idGH);
            if (userCart == null)
            {
                userCart = new GioHang { Id = idGH };
                _context.gioHangs.Add(userCart);
                _context.SaveChanges();
            }

            var sanPham = _context.sanPhams.Find(id);
            if (sanPham == null || sanPham.SoLuong < quantity)
            {
                TempData["Error"] = "Sản phẩm không tồn tại hoặc số lượng không đủ.";
                return RedirectToAction("ProductList");
            }

            var existingItem = _context.gioHangChiTiets.FirstOrDefault(p => p.IdGH == idGH && p.IdSP == id);
            if (existingItem == null)
            {
                var ghct = new GioHangChiTiet
                {
                    Id = Guid.NewGuid(),
                    IdSP = id,
                    IdGH = idGH,
                    SoLuong = quantity,
                };
                _context.gioHangChiTiets.Add(ghct);
            }
            else
            {
                existingItem.SoLuong += quantity;
                _context.gioHangChiTiets.Update(existingItem);
            }

            sanPham.SoLuong -= quantity;
            sanPham.TrangThai = sanPham.SoLuong > 0;
            _context.sanPhams.Update(sanPham);

            _context.SaveChanges();

            TempData["Success"] = "Sản phẩm đã được thêm vào giỏ hàng thành công.";
            return RedirectToAction("ProductList");
        }
    }
}
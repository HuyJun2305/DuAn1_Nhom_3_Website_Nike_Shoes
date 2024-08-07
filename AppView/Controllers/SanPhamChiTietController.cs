using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext _context;

        public SanPhamChiTietController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action để hiển thị thông tin chi tiết của sản phẩm
        public async Task<IActionResult> ViewSanPhamChiTiet(Guid id)
        {
            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            var product = await _context.sanPhams
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Tạo đối tượng ViewModel
            var viewModel = new ProductDetailViewModel
            {
                Id = product.Id,
                Ten = product.Ten,
                MoTa = product.MoTa,
                ImgFile = product.ImgFile,
                Gia = product.Gia,
                SoLuong = product.SoLuong
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddToCart(Guid id, int quantity)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Login", "Account");
            }

            var idKH = Guid.Parse(userId);

            // Tìm giỏ hàng của người dùng
            var userCart = _context.gioHangs.FirstOrDefault(g => g.IdKH == idKH);
            if (userCart == null)
            {
                // Tạo mới giỏ hàng nếu chưa tồn tại
                userCart = new GioHang
                {
                    Id = Guid.NewGuid(), // Tạo GUID mới cho Id của giỏ hàng
                    IdKH = idKH,
                    TongTien = 0 // Bạn có thể thiết lập giá trị mặc định cho các thuộc tính khác nếu cần
                };
                _context.gioHangs.Add(userCart);
                _context.SaveChanges();
            }

            // Tìm sản phẩm
            var sanPham = _context.sanPhams.Find(id);
            if (sanPham == null || sanPham.SoLuong < quantity)
            {
                TempData["Error"] = "Sản phẩm không tồn tại hoặc số lượng không đủ.";
                return RedirectToAction("ProductList", "ViewKhachHang");
            }

            // Tìm chi tiết giỏ hàng
            var existingItem = _context.gioHangChiTiets
                .FirstOrDefault(p => p.IdGH == userCart.Id && p.IdSP == id);

            if (existingItem == null)
            {
                // Thêm mới chi tiết giỏ hàng nếu chưa có
                var ghct = new GioHangChiTiet
                {
                    Id = Guid.NewGuid(),
                    IdSP = id,
                    IdGH = userCart.Id,
                    SoLuong = quantity,
                };
                _context.gioHangChiTiets.Add(ghct);
            }
            else
            {
                // Cập nhật số lượng nếu chi tiết giỏ hàng đã tồn tại
                existingItem.SoLuong += quantity;
                _context.gioHangChiTiets.Update(existingItem);
            }

            // Cập nhật số lượng sản phẩm
            sanPham.SoLuong -= quantity;
            sanPham.TrangThai = sanPham.SoLuong > 0;
            _context.sanPhams.Update(sanPham);

            _context.SaveChanges();

            TempData["Success"] = "Sản phẩm đã được thêm vào giỏ hàng thành công.";
            return RedirectToAction("ProductList", "ViewKhachHang");
        }






    }
}

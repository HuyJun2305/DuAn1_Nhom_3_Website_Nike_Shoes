using AppView.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppView.Models;


namespace AppView.Controllers
{
    [Authorize(Roles = "User")]
    public class DonHangKhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonHangKhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Xem danh sách đơn hàng của khách hàng
        public async Task<IActionResult> ViewDonHang()
        {
            var userId = HttpContext.Session.GetString("userId");

            // Kiểm tra xem người dùng đã đăng nhập
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để xem danh sách đơn hàng.";
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra tính hợp lệ của ID người dùng
            if (!Guid.TryParse(userId, out var khachHangId))
            {
                TempData["Error"] = "ID người dùng không hợp lệ.";
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách đơn hàng của người dùng từ cơ sở dữ liệu
            var donHangs = await _context.donHangs
                .Where(dh => dh.IdKH == khachHangId)
                .Include(dh => dh.HoaDon) // Bao gồm thông tin hóa đơn nếu cần
                .Include(dh => dh.ChiTietDonHangs) // Bao gồm chi tiết đơn hàng
                    .ThenInclude(ct => ct.SanPham) // Bao gồm thông tin sản phẩm
                .ToListAsync();

            // Trả về view với danh sách đơn hàng
            return View(donHangs);
        }

        public async Task<IActionResult> ChiTietDonHang(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var donHang = await _context.donHangs
                .Include(d => d.KhachHang)
                .Include(d => d.HoaDon)
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }





        // Mua lại đơn hàng
        [HttpPost]
        public async Task<IActionResult> MuaLaiDonHang(Guid id)
        {
            var donHang = await _context.donHangs
                .Include(dh => dh.HoaDonChiTiets)
                .FirstOrDefaultAsync(dh => dh.Id == id);

            if (donHang == null || donHang.TrangThaiDonHang != "Đã hủy")
            {
                return NotFound();
            }

            // Tạo một đơn hàng mới từ đơn hàng đã hủy
            var newDonHang = new DonHang
            {
                Id = Guid.NewGuid(),
                IdKH = donHang.IdKH,
                NgayTao = DateTime.Now,
                TrangThaiDonHang = "Chờ xác nhận",
                TongTien = donHang.TongTien,
                TenNguoiNhan = donHang.TenNguoiNhan,
                SoDienThoai = donHang.SoDienThoai,
                DiaChi = donHang.DiaChi,
                PhuongThucThanhToan = donHang.PhuongThucThanhToan,
                HoaDonChiTiets = donHang.HoaDonChiTiets.Select(hdct => new HoaDonChiTiet
                {
                    Id = Guid.NewGuid(),
                    Gia = hdct.Gia,
                    SoLuong = hdct.SoLuong,
                    IdSP = hdct.IdSP,
                    IdHD = donHang.IdHD.HasValue ? donHang.IdHD.Value : Guid.Empty,
                    IdTT = hdct.IdTT
                }).ToList()
            };

            _context.donHangs.Add(newDonHang);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ViewDonHang));
        }

        // Thanh toán lại
        [HttpPost]
        public async Task<IActionResult> ThanhToanLai(Guid id)
        {
            var donHang = await _context.donHangs
                .FirstOrDefaultAsync(dh => dh.Id == id);

            if (donHang == null || donHang.TrangThaiDonHang != "Chờ xác nhận")
            {
                return NotFound();
            }

            // Logic thanh toán lại
            // Ví dụ: cập nhật trạng thái đơn hàng sau khi thanh toán
            donHang.TrangThaiDonHang = "Đã thanh toán"; // Hoặc trạng thái khác phù hợp
            _context.donHangs.Update(donHang);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ViewDonHang));
        }
    }
}
using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppView.Controllers
{
    public class GioHangChiTietController : Controller
    {
        private readonly AppDbContext _context;

        public GioHangChiTietController(AppDbContext context)
        {
            _context = context;
        }

        // Xem giỏ hàng
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Login", "User");
            }

            var cartItems = await _context.gioHangChiTiets
                                          .Where(ct => ct.IdGH == Guid.Parse(userId))
                                          .Include(ct => ct.SanPham)
                                          .ToListAsync();

            return View(cartItems);
        }

        // Xác nhận thanh toán
        public async Task<IActionResult> XacNhanThanhToan()
        {
            var userId = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để thực hiện thanh toán.";
                return RedirectToAction("Login", "User");
            }

            var cartItems = await _context.gioHangChiTiets
                                          .Where(ct => ct.IdGH == Guid.Parse(userId))
                                          .Include(ct => ct.SanPham)
                                          .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Giỏ hàng của bạn trống.";
                return RedirectToAction("Index");
            }

            var totalAmount = cartItems.Sum(ct => ct.SanPham.Gia * ct.SoLuong);

            var hoaDon = new HoaDon
            {
                Id = Guid.NewGuid(),
                NgayTao = DateTime.Now,
                TongTien = totalAmount,
                TrangThai = false, // Trạng thái chưa thanh toán
                IdKH = Guid.Parse(userId)
            };

            _context.hoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            // Chuyển hướng đến trang thanh toán QR Pay
            return RedirectToAction("ThanhToan", new { id = hoaDon.Id });
        }

        // Xử lý thanh toán và lưu hóa đơn
        public async Task<IActionResult> ThanhToan(Guid id)
        {
            var hoaDon = await _context.hoaDons.FindAsync(id);

            if (hoaDon == null)
            {
                TempData["Error"] = "Hóa đơn không tồn tại.";
                return RedirectToAction("Index");
            }

            // Xử lý thanh toán bằng QR Pay (giả định)
            bool isPaymentSuccessful = ProcessQRPayment(id);

            if (isPaymentSuccessful)
            {
                hoaDon.TrangThai = true; // Đánh dấu hóa đơn là đã thanh toán thành công
                _context.hoaDons.Update(hoaDon);

                var cartItems = await _context.gioHangChiTiets
                                              .Where(ct => ct.IdGH == hoaDon.IdKH)
                                              .Include(ct => ct.SanPham)
                                              .ToListAsync();

                foreach (var item in cartItems)
                {
                    var hoaDonChiTiet = new HoaDonChiTiet
                    {
                        Id = Guid.NewGuid(),
                        IdHD = hoaDon.Id,
                        IdSP = item.IdSP,
                        SoLuong = item.SoLuong,
                        Gia = item.SanPham.Gia
                    };

                    _context.hoaDonChiTiets.Add(hoaDonChiTiet);
                    _context.gioHangChiTiets.Remove(item); // Xóa sản phẩm khỏi giỏ hàng
                }

                await _context.SaveChangesAsync();

                // Hiển thị hóa đơn
                return RedirectToAction("BillDetails", new { id = hoaDon.Id });
            }
            else
            {
                TempData["Error"] = "Thanh toán không thành công. Vui lòng thử lại.";
                return RedirectToAction("Index");
            }
        }

        // Hiển thị hóa đơn
        public async Task<IActionResult> BillDetails(Guid id)
        {
            var hoaDon = await _context.hoaDons
                                       .Include(hd => hd.HoaDonChiTiets)
                                           .ThenInclude(hdct => hdct.SanPham)
                                       .FirstOrDefaultAsync(hd => hd.Id == id);

            if (hoaDon == null)
            {
                TempData["Error"] = "Hóa đơn không tồn tại.";
                return RedirectToAction("Index");
            }

            var khachHang = await _context.khachHangs.FindAsync(hoaDon.IdKH);

            var viewModel = new HoaDonChiTietViewModel
            {
                HoaDon = hoaDon,
                KhachHang = khachHang,
                HoaDonChiTiets = hoaDon.HoaDonChiTiets.ToList()
            };

            return View(viewModel);
        }

        private bool ProcessQRPayment(Guid hoaDonId)
        {
            // Giả lập xử lý thanh toán QR Pay
            // Bạn cần tích hợp với dịch vụ thanh toán thực tế ở đây
            return true; // Giả sử thanh toán thành công
        }
    }
}

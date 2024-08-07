using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ThanhToanController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Checkout()
        {
            // Lấy thông tin người dùng hiện tại
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để thanh toán.";
                return RedirectToAction("Login", "Account");
            }

            // Lấy các sản phẩm trong giỏ hàng của người dùng từ cơ sở dữ liệu
            var cartItems = await _context.gioHangChiTiets
                                          .Where(ct => ct.GioHang.IdKH == user.Id)
                                          .Include(ct => ct.SanPham)
                                          .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index", "GioHangChiTiet");
            }

            // Tạo mô hình CheckoutViewModel
            var model = new CheckoutViewModel
            {
                CartItems = cartItems.Select(item => new CartItemViewModel
                {
                    ProductId = item.IdSP,
                    ProductName = item.SanPham.Ten,
                    Price = item.SanPham.Gia,
                    Quantity = item.SoLuong
                }).ToList(),
                TotalAmount = cartItems.Sum(item => item.SanPham.Gia * item.SoLuong)
            };

            return View(model);
        }

        public async Task<IActionResult> CompleteCheckout(CheckoutViewModel model)
        {
            // Lấy thông tin người dùng hiện tại
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để hoàn tất đơn hàng.";
                return RedirectToAction("Login", "Account");
            }

            // Lấy các sản phẩm trong giỏ hàng của người dùng từ cơ sở dữ liệu
            var cartItems = await _context.gioHangChiTiets
                                          .Where(ct => ct.GioHang.IdKH == user.Id)
                                          .Include(ct => ct.SanPham)
                                          .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index", "GioHangChiTiet");
            }

            // Tính tổng tiền
            decimal totalAmount = cartItems.Sum(item => item.SanPham.Gia * item.SoLuong);

            // Tạo hóa đơn mới
            var invoice = new HoaDon
            {
                Id = Guid.NewGuid(),
                NgayTao = DateTime.Now,
                TongTien = totalAmount,
                TrangThai = false, // Trang thái chưa thanh toán hoặc cần điều chỉnh
                IdKH = user.Id // Sử dụng IdKH để liên kết với người dùng

            };

            _context.hoaDons.Add(invoice);
            await _context.SaveChangesAsync();



            // Tạo đơn hàng mới
            var order = new DonHang
            {
                Id = Guid.NewGuid(),
                IdKH = user.Id,
                NgayTao = DateTime.Now,
                TrangThaiDonHang = "Pending", // Hoặc trạng thái khác nếu cần
                TongTien = totalAmount,
                TenNguoiNhan = model.RecipientName,
                SoDienThoai = model.PhoneNumber,
                DiaChi = model.Address,
                PhuongThucThanhToan = model.PaymentMethod,
                IdHD = invoice.Id
            };

            // Lưu đơn hàng vào cơ sở dữ liệu
            _context.donHangs.Add(order);
            await _context.SaveChangesAsync();

            // Thêm chi tiết đơn hàng
            foreach (var item in cartItems)
            {
                var orderDetail = new ChiTietDonHang
                {
                    Id = Guid.NewGuid(),
                    IdDH = order.Id,
                    IdSP = item.IdSP,
                    SoLuong = item.SoLuong,
                    Gia = item.SanPham.Gia
                };

                _context.chiTietDonHangs.Add(orderDetail);
            }

            

            // Cập nhật đơn hàng với ID hóa đơn
            order.IdHD = invoice.Id; // Đảm bảo cột IdHD tồn tại trong bảng DonHangs
            _context.donHangs.Update(order);
            await _context.SaveChangesAsync();

            // Xóa các sản phẩm trong giỏ hàng
            _context.gioHangChiTiets.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            // Thông báo cho admin nếu cần
            await NotifyAdmin(order);

            TempData["Success"] = "Đơn hàng của bạn đã được đặt thành công. Vui lòng chờ xác nhận từ quản trị viên.";
            return RedirectToAction("ChiTietDonHang","DonHangKhachHang", new { id = order.Id });
        }






        private async Task NotifyAdmin(DonHang order)
        {
            // Implement logic to notify admin about the new order
        }

    }
}

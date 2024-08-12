    using AppView.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class DonHangAdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DonHangAdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Xem danh sách đơn hàng
    public async Task<IActionResult> Index()
    {
        var orders = await _context.donHangs
                                   .Include(o => o.KhachHang)
                                   .Where(o => o.TrangThaiDonHang == "Chưa xác nhận")
                                   .ToListAsync();
        return View(orders);
    }

    public async Task<IActionResult> ConfirmOrder(Guid id)
    {
        // Lấy tên người dùng từ Session
        var username = HttpContext.Session.GetString("username");

        if (string.IsNullOrEmpty(username))
        {
            TempData["Error"] = "Bạn cần đăng nhập để tiếp tục.";
            return RedirectToAction("Login", "Account");
        }

        // Tìm admin trong cơ sở dữ liệu
        var admin = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (admin == null)
        {
            TempData["Error"] = "Không tìm thấy thông tin admin.";
            return RedirectToAction("Login", "Account");
        }

        // Tìm đơn hàng bằng Id
        var donHang = await _context.donHangs
            .Include(dh => dh.KhachHang)
            .Include(dh => dh.ChiTietDonHangs)
                .ThenInclude(ct => ct.SanPham)
            .FirstOrDefaultAsync(dh => dh.Id == id);

        if (donHang == null)
        {
            TempData["Error"] = "Không tìm thấy đơn hàng.";
            return RedirectToAction("Index");
        }

        // Tạo hóa đơn mới
        var hoaDon = new HoaDon
        {
            Id = Guid.NewGuid(),
            NgayTao = DateTime.Now,
            IdKH = donHang.IdKH,
            TongTien = donHang.TongTien,
            NguoiBan = admin.UserName, // Gán người bán là tên người dùng
            IdDH = donHang.Id // Liên kết với đơn hàng hiện tại
        };

        // Kiểm tra phương thức thanh toán và cập nhật trạng thái hóa đơn
        hoaDon.TrangThai = donHang.PhuongThucThanhToan == "VNPay";

        // Thêm hóa đơn vào cơ sở dữ liệu
        _context.hoaDons.Add(hoaDon);
        await _context.SaveChangesAsync();

        // Cập nhật IdHD trong đơn hàng để liên kết với hóa đơn vừa tạo
        donHang.IdHD = hoaDon.Id;
        donHang.TrangThaiDonHang = "Đã xác nhận"; // Cập nhật trạng thái đơn hàng
        _context.donHangs.Update(donHang);
        await _context.SaveChangesAsync();

        // Tạo chi tiết hóa đơn từ đơn hàng
        var hoaDonChiTiets = donHang.ChiTietDonHangs.Select(ct => new HoaDonChiTiet
        {
            Id = Guid.NewGuid(),
            IdHD = hoaDon.Id,
            IdSP = ct.SanPham.Id,
        }).ToList();

        _context.hoaDonChiTiets.AddRange(hoaDonChiTiets);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Đơn hàng đã được xác nhận và hóa đơn đã được tạo.";

        // Chuyển hướng đến trang quản lý hóa đơn của admin
        return RedirectToAction("Index", "HoaDonAdmin");
    }







    // Hủy đơn hàng
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var order = await _context.donHangs.FindAsync(id);
        if (order == null)
        {
            TempData["Error"] = "Không tìm thấy đơn hàng.";
            return RedirectToAction("Index");
        }

        order.TrangThaiDonHang = "Đã hủy";
        _context.donHangs.Update(order);
        await _context.SaveChangesAsync();

        // Notify customer
        await NotifyCustomer(order);

        TempData["Success"] = "Đơn hàng đã được hủy thành công.";
        return RedirectToAction("Index");
    }

    



    private async Task NotifyCustomer(DonHang order)
    {
        // Logic to send notification to customer
        var customer = await _userManager.FindByIdAsync(order.IdKH.ToString());
        if (customer != null)
        {
            // Example: Send email or other notifications to customer
            // await _emailSender.SendEmailAsync(customer.Email, "Order Confirmed", "Your order has been confirmed.");
        }
    }
}

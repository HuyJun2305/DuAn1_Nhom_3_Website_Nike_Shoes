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
                                   .Where(o => o.TrangThaiDonHang == "Pending")
                                   .ToListAsync();
        return View(orders);
    }

    public async Task<IActionResult> ConfirmOrder(Guid id)
    {
        var order = await _context.donHangs.FindAsync(id);
        if (order == null)
        {
            TempData["Error"] = "Không tìm thấy đơn hàng.";
            return RedirectToAction("Index");
        }

        order.TrangThaiDonHang = "Confirmed";
        await _context.SaveChangesAsync();

        // Notify customer
        await NotifyCustomer(order);

        TempData["Success"] = "Đơn hàng đã được xác nhận thành công.";
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

using AppView.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class HoaDonAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HoaDonAdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy thông tin người dùng từ Session nếu cần
            var adminId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(adminId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để tiếp tục.";
                return RedirectToAction("Login", "Account");
            }

            // Lấy tất cả hóa đơn từ cơ sở dữ liệu, bao gồm thông tin đơn hàng và các chi tiết hóa đơn
            var hoaDons = await _context.hoaDons
                .Include(hd => hd.KhachHang) // Thêm thông tin khách hàng nếu cần
                .Include(hd => hd.HoaDonChiTiets)
                    .ThenInclude(hdct => hdct.SanPham) // Thêm thông tin sản phẩm liên quan
                .Include(hd => hd.DonHang) // Bao gồm thông tin đơn hàng liên quan
                .ToListAsync();

            return View(hoaDons);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(Guid id, string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus) || !new[] { "Đang vận chuyển", "Đã vận chuyển" }.Contains(newStatus))
            {
                TempData["Error"] = "Trạng thái không hợp lệ.";
                return RedirectToAction("Index");
            }

            var donHang = await _context.donHangs.FindAsync(id);

            if (donHang == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("Index");
            }

            donHang.TrangThaiDonHang = newStatus;
            _context.donHangs.Update(donHang);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Trạng thái đơn hàng đã được cập nhật.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PayInvoice(Guid id)
        {
            // Tìm hóa đơn theo ID
            var hoaDon = await _context.hoaDons
                .Include(hd => hd.DonHang) // Bao gồm cả thông tin đơn hàng liên quan
                .FirstOrDefaultAsync(hd => hd.Id == id);

            if (hoaDon == null)
            {
                TempData["Error"] = "Không tìm thấy hóa đơn.";
                return RedirectToAction("Index");
            }

            // Cập nhật trạng thái hóa đơn
            hoaDon.TrangThai = true; // Chuyển trạng thái thành "Đã thanh toán"
            _context.hoaDons.Update(hoaDon);

            // Cập nhật trạng thái đơn hàng liên quan
            if (hoaDon.DonHang != null)
            {
                hoaDon.DonHang.TrangThaiDonHang = "Đã thanh toán";
                _context.donHangs.Update(hoaDon.DonHang); // Cập nhật trạng thái đơn hàng
            }

            await _context.SaveChangesAsync(); // Lưu tất cả thay đổi vào cơ sở dữ liệu

            TempData["Success"] = "Hóa đơn và đơn hàng đã được thanh toán thành công.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelInvoice(Guid id)
        {
            // Tìm hóa đơn theo ID
            var hoaDon = await _context.hoaDons
                .Include(hd => hd.DonHang) // Bao gồm thông tin đơn hàng liên quan
                .FirstOrDefaultAsync(hd => hd.Id == id);

            if (hoaDon == null)
            {
                TempData["Error"] = "Không tìm thấy hóa đơn.";
                return RedirectToAction("Index");
            }

            // Cập nhật trạng thái hóa đơn
            hoaDon.TrangThai = false; // Chuyển trạng thái thành "Chưa thanh toán"
            _context.hoaDons.Update(hoaDon);

            // Cập nhật trạng thái đơn hàng liên quan nếu cần
            if (hoaDon.DonHang != null)
            {
                hoaDon.DonHang.TrangThaiDonHang = "Đã hủy thanh toán";
                _context.donHangs.Update(hoaDon.DonHang);
            }

            await _context.SaveChangesAsync(); // Lưu tất cả thay đổi vào cơ sở dữ liệu

            TempData["Success"] = "Hóa đơn và đơn hàng đã được hủy thanh toán thành công.";
            return RedirectToAction("Index");
        }


    }
}

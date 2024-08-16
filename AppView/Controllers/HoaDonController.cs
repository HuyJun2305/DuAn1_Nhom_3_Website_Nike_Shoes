using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PdfService _pdfService;
        public HoaDonController(ApplicationDbContext context, PdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }
     
        public async Task<IActionResult> DanhSachHoaDon()
        {
            // Lấy ID người dùng từ Session
            var userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để tiếp tục.";
                return RedirectToAction("Login", "Account");
            }

            if (!Guid.TryParse(userId, out var khachHangId))
            {
                TempData["Error"] = "ID người dùng không hợp lệ.";
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách hóa đơn của khách hàng
            var hoaDons = await _context.hoaDons
                .Where(hd => hd.IdKH == khachHangId)
                .Include(hd => hd.HoaDonChiTiets)
                .ThenInclude(hdct => hdct.SanPham)
                .ToListAsync();

            // Kiểm tra nếu không có hóa đơn nào
            if (hoaDons == null || !hoaDons.Any())
            {
                TempData["Info"] = "Bạn chưa có hóa đơn nào.";
                return View(new List<HoaDon>());
            }

            return View(hoaDons);
        }
        [HttpGet]
        public async Task<IActionResult> ChiTiet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var hoaDon = await _context.hoaDons
                .Include(d => d.KhachHang)  // Bao gồm thông tin khách hàng
                .Include(d => d.DonHang)   // Bao gồm thông tin đơn hàng liên quan
                    .ThenInclude(dh => dh.ChiTietDonHangs) // Bao gồm chi tiết đơn hàng
                        .ThenInclude(ct => ct.SanPham) // Bao gồm thông tin sản phẩm
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }




        public IActionResult DownloadInvoice(Guid id)
        {
            var hoaDon = _context.hoaDons
                .Include(hd => hd.DonHang) // Thay đổi để lấy thông tin đơn hàng liên kết
                .ThenInclude(dh => dh.ChiTietDonHangs)
                .FirstOrDefault(hd => hd.Id == id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            var pdfBytes = _pdfService.CreateInvoicePdf(hoaDon);
            return File(pdfBytes, "application/pdf", "invoice.pdf");
        }

        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var data = _context.sanPhams.Find(id);
        //    return View(data);

        //}
        //public async Task<IActionResult> HoaDonChiTiet(Guid id)
        //{
        //    var hoaDon = await _context.hoaDons
        //                               .Include(hd => hd.HoaDonChiTiets)
        //                               .ThenInclude(hdct => hdct.SanPham)
        //                               .FirstOrDefaultAsync(hd => hd.Id == id);

        //    if (hoaDon == null)
        //    {
        //        TempData["Error"] = "Hóa đơn không tồn tại.";
        //        return RedirectToAction("Index");
        //    }

        //    var khachHang = await _context.khachHangs.FindAsync(hoaDon.IdKH);

        //    var viewModel = new HoaDonChiTietViewModel
        //    {
        //        HoaDon = hoaDon,
        //        KhachHang = khachHang,
        //        HoaDonChiTiets = hoaDon.HoaDonChiTiets.ToList(),
        //        SanPham = hoaDon.HoaDonChiTiets.Select(hdct => hdct.SanPham).Distinct().ToList()
        //    };

        //    return View(viewModel);
        //}

    }

}

using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PdfService _pdfService;
        public HoaDonController(AppDbContext context, PdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }
        //    public async Task<IActionResult> HoaDon(Guid id)
        //    {
        //        if (id == Guid.Empty)
        //        {
        //            return BadRequest("Invalid invoice ID.");
        //        }

        //        var hoaDon = await _context.hoaDons
        //                                   .Include(hd => hd.HoaDonChiTiets)
        //                                   .ThenInclude(ct => ct.SanPham)
        //                                   .FirstOrDefaultAsync(hd => hd.Id == id);

        //        if (hoaDon == null)
        //        {
        //            return NotFound("Invoice not found.");
        //        }

        //        return View(hoaDon);
        //    }
        public async Task<IActionResult> DanhSachHoaDon()
        {
            var userId = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để tiếp tục.";
                return RedirectToAction("Login", "User");
            }

            var khachHangId = Guid.Parse(userId);
            var hoaDons = await _context.hoaDons
                                       .Where(hd => hd.IdKH == khachHangId)
                                       .Include(hd => hd.HoaDonChiTiets)
                                       .ThenInclude(hdct => hdct.SanPham)
                                       .ToListAsync();

            return View(hoaDons);
        }

        public IActionResult DownloadInvoice(Guid id)
        {
            var hoaDon = _context.hoaDons
                .Include(hd => hd.HoaDonChiTiets)
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
        //        SanPhams = hoaDon.HoaDonChiTiets.Select(hdct => hdct.SanPham).Distinct().ToList()
        //    };

        //    return View(viewModel);
        //}

    }

}

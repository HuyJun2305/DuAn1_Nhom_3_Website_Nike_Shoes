using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class TraHangAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TraHangAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action Index để hiển thị danh sách trả hàng
        public async Task<IActionResult> LichSuTraHang()
        {
            var chiTietTraHangs = await _context.chiTietTraHangs
                .Include(ct => ct.TraHang) // Nếu cần lấy thêm thông tin liên quan
                .ToListAsync();

            var viewModel = chiTietTraHangs.Select(ct => new ChiTietTraHangViewModel
            {
                Id = ct.Id,
                IdTraHang = ct.IdTraHang,
                NguoiXuLy = ct.NguoiXuLy,
                NgayXacNhan = ct.NgayXacNhan,
                DiaChi = ct.TraHang.DiaChi,
                SoDienThoai = ct.TraHang.SoDienThoai,
                GhiChu = ct.TraHang.GhiChu,
                PhiHoanTra = ct.TraHang.PhiHoanTra,
                TrangThaiTraHang = ct.TraHang.TrangThaiTraHang
            }).ToList();

            return View(viewModel);
        }


        public async Task<IActionResult> Index()
        {
            // Lấy danh sách trả hàng từ cơ sở dữ liệu có trạng thái "Chờ xác nhận", bao gồm cả thông tin đơn hàng liên quan
            var traHangs = await _context.traHangs
                .Include(th => th.DonHang)
                .Where(th => th.TrangThaiTraHang == "Chờ xác nhận")
                .ToListAsync();

            return View(traHangs);
        }


        [HttpPost]
        public async Task<IActionResult> DongYTraHang(Guid id)
        {
            var traHang = await _context.traHangs
                .Include(th => th.ChiTietTraHangs) // Bao gồm chi tiết trả hàng
                .FirstOrDefaultAsync(th => th.Id == id);

            if (traHang == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin trả hàng.";
                return RedirectToAction("Index");
            }

            // Lấy thông tin người xử lý từ session
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Không xác định được người xử lý.";
                return RedirectToAction("Index");
            }

            // Cập nhật trạng thái trả hàng
            traHang.TrangThaiTraHang = "Đã đồng ý";

            // Tạo bản ghi chi tiết trả hàng mới
            var chiTietTraHang = new ChiTietTraHang
            {
                Id = Guid.NewGuid(),
                IdTraHang = traHang.Id,
                NguoiXuLy = username, // Lưu thông tin người xử lý từ session
                NgayXacNhan = DateTime.Now // Gán ngày xác nhận
            };

            // Thêm chi tiết trả hàng vào cơ sở dữ liệu
            _context.chiTietTraHangs.Add(chiTietTraHang);

            // Cập nhật trạng thái trả hàng
            _context.traHangs.Update(traHang);

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã đồng ý trả hàng.";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> TuChoiTraHang(Guid id)
        {
            var traHang = await _context.traHangs
                .Include(th => th.ChiTietTraHangs) // Bao gồm chi tiết trả hàng
                .FirstOrDefaultAsync(th => th.Id == id);

            if (traHang == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin trả hàng.";
                return RedirectToAction("Index");
            }

            // Lấy thông tin người xử lý từ session
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Không xác định được người xử lý.";
                return RedirectToAction("Index");
            }

            // Cập nhật trạng thái trả hàng
            traHang.TrangThaiTraHang = "Đã từ chối";

            // Tạo bản ghi chi tiết trả hàng mới
            var chiTietTraHang = new ChiTietTraHang
            {
                Id = Guid.NewGuid(),
                IdTraHang = traHang.Id,
                NguoiXuLy = username, // Lưu thông tin người xử lý từ session
                NgayXacNhan = DateTime.Now // Gán ngày xác nhận
            };

            // Thêm chi tiết trả hàng vào cơ sở dữ liệu
            _context.chiTietTraHangs.Add(chiTietTraHang);

            // Cập nhật trạng thái trả hàng
            _context.traHangs.Update(traHang);

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã từ chối trả hàng.";
            return RedirectToAction("Index");
        }

    }

}


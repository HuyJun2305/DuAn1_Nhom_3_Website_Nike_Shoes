using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    [Authorize(Roles = "User")]
    public class TraHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TraHangController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            // Lấy thông tin khách hàng từ Session
            var userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để xem thông tin trả hàng.";
                return RedirectToAction("Login", "Account"); // Hoặc trang khác nếu cần
            }

            // Lấy danh sách trả hàng liên quan đến khách hàng từ cơ sở dữ liệu
            var traHangList = await _context.traHangs
                .Include(th => th.DonHang) // Bao gồm thông tin đơn hàng liên quan
                .Where(th => th.DonHang.IdKH == new Guid(userId)) // Lọc theo khách hàng
                .Select(th => new TraHangListItemViewModel
                {
                    Id = th.Id,
                    IdDonHang = th.IdDH,
                    DiaChi = th.DiaChi,
                    SoDienThoai = th.SoDienThoai,
                    GhiChu = th.GhiChu,
                    PhiHoanTra = th.PhiHoanTra,
                    NgayTraHang = th.NgayTraHang,
                    TrangThaiDonHang = th.DonHang.TrangThaiDonHang, // Trạng thái đơn hàng
                    TrangThaiTraHang = th.TrangThaiTraHang
                })
                .ToListAsync();

            var viewModel = new TraHangIndexViewModel
            {
                TraHangList = traHangList
            };

            return View(viewModel);
        }


        public IActionResult ChiTiet(Guid id)
        {
            var traHang = _context.traHangs
                .Include(th => th.DonHang)
                .FirstOrDefault(th => th.Id == id);

            if (traHang == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin trả hàng.";
                return RedirectToAction("Index");
            }

            var viewModel = new TraHangListItemViewModel
            {
                Id = traHang.Id,
                IdDonHang = traHang.IdDH,
                DiaChi = traHang.DiaChi,
                SoDienThoai = traHang.SoDienThoai,
                GhiChu = traHang.GhiChu,
                PhiHoanTra = traHang.PhiHoanTra,
                NgayTraHang = traHang.NgayTraHang,
                TrangThaiDonHang = traHang.DonHang.TrangThaiDonHang
            };

            return View(viewModel);
        }
        public IActionResult NhapThongTinTraHang(Guid id, decimal phiHoanTra)
        {
            // Tìm đơn hàng bằng Id
            var donHang = _context.donHangs
                .Include(dh => dh.KhachHang)
                .FirstOrDefault(dh => dh.Id == id);

            if (donHang == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("Index");
            }

            // Kiểm tra địa chỉ và số điện thoại có hợp lệ không
            if (string.IsNullOrWhiteSpace(donHang.DiaChi) || string.IsNullOrWhiteSpace(donHang.SoDienThoai))
            {
                TempData["Error"] = "Thông tin địa chỉ và số điện thoại không hợp lệ.";
                return RedirectToAction("ViewDonHang");
            }

            var viewModel = new NhapThongTinTraHangViewModel
            {
                IdDonHang = donHang.Id,
                TenNguoiTra = donHang.TenNguoiNhan,
                NgayTao = donHang.NgayTao,
                TongTien = donHang.TongTien,
                PhuongThucThanhToan = donHang.PhuongThucThanhToan,
                DiaChi = donHang.DiaChi,
                SoDienThoai = donHang.SoDienThoai,
                PhiHoanTra = phiHoanTra // Truyền phí hoàn trả vào view model
            };

            return View(viewModel);
        }





        [HttpPost]
        public async Task<IActionResult> XacNhanTraHang(NhapThongTinTraHangViewModel model)
        {
            // Tìm đơn hàng dựa trên IdDonHang
            var donHang = await _context.donHangs
                .Include(dh => dh.HoaDon)
                .FirstOrDefaultAsync(dh => dh.Id == model.IdDonHang);

            if (donHang == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("Index");
            }

            // Cập nhật trạng thái đơn hàng và các thông tin khác
            donHang.TrangThaiDonHang = "Đã trả hàng";
            donHang.DiaChi = model.DiaChi;
            donHang.SoDienThoai = model.SoDienThoai;

            if (donHang.HoaDon != null)
            {
                donHang.HoaDon.TrangThai = false; // Đánh dấu hóa đơn là không còn hiệu lực
                _context.hoaDons.Update(donHang.HoaDon); // Cập nhật hóa đơn
            }

            // Kiểm tra xem đã có bản ghi trả hàng với IdDH chưa
            var existingTraHang = await _context.traHangs
                .FirstOrDefaultAsync(th => th.IdDH == model.IdDonHang);

            if (existingTraHang != null)
            {
                // Cập nhật bản ghi trả hàng đã tồn tại
                existingTraHang.DiaChi = model.DiaChi;
                existingTraHang.SoDienThoai = model.SoDienThoai;
                existingTraHang.GhiChu = model.GhiChu;
                existingTraHang.PhiHoanTra = model.PhiHoanTra; // PhiHoanTra có thể được tính từ model hoặc từ đơn hàng
                existingTraHang.NgayTraHang = DateTime.Now;
                existingTraHang.TrangThaiTraHang = "Đã xác nhận"; // Cập nhật trạng thái trả hàng là "Đã xác nhận"

                _context.traHangs.Update(existingTraHang); // Cập nhật bản ghi trả hàng đã tồn tại
            }
            else
            {
                // Thêm bản ghi trả hàng mới
                var traHang = new TraHang
                {
                    Id = Guid.NewGuid(),
                    IdDH = model.IdDonHang,
                    DiaChi = model.DiaChi,
                    SoDienThoai = model.SoDienThoai,
                    GhiChu = model.GhiChu,
                    PhiHoanTra = donHang.TongTien * 10 / 100, // Tính toán phí hoàn trả (10%)
                    NgayTraHang = DateTime.Now,
                    TrangThaiTraHang = "Chờ xác nhận", // Đặt trạng thái trả hàng là "Chờ xác nhận"

                };

                _context.traHangs.Add(traHang); // Thêm bản ghi trả hàng mới
            }

            // Cập nhật đơn hàng
            _context.donHangs.Update(donHang);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            TempData["Success"] = "Quá trình trả hàng đã được xác nhận.";
            return RedirectToAction("ViewDonHang", "DonHangKhachHang");
        }



    }
}

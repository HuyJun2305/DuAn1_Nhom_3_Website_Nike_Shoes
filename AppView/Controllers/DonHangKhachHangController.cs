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
                TempData["DH-Error"] = "Bạn cần đăng nhập để xem danh sách đơn hàng.";
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra tính hợp lệ của ID người dùng
            if (!Guid.TryParse(userId, out var khachHangId))
            {
                TempData["DH-Error"] = "ID người dùng không hợp lệ.";
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách đơn hàng của người dùng từ cơ sở dữ liệu
            var donHangs = await _context.donHangs
                .Where(dh => dh.IdKH == khachHangId)
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
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }


        public async Task<IActionResult> MuaLai(Guid id)
        {
            // Lấy thông tin đơn hàng từ cơ sở dữ liệu
            var donHang = await _context.donHangs
                .Include(dh => dh.ChiTietDonHangs) // Bao gồm ChiTietDonHangs để lấy thông tin chi tiết đơn hàng
                    .ThenInclude(ct => ct.SanPham) // Bao gồm thông tin sản phẩm
                .FirstOrDefaultAsync(dh => dh.Id == id);

            if (donHang == null)
            {
                TempData["DH-Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("ViewDonHang");
            }

            // Kiểm tra trạng thái đơn hàng và chỉ xóa nếu trạng thái là "Đã hủy"
            if (donHang.TrangThaiDonHang != "Đã hủy")
            {
                TempData["DH-Error"] = "Chỉ đơn hàng có trạng thái 'Đã hủy' mới có thể mua lại.";
                return RedirectToAction("ViewDonHang");
            }

            // Xóa đơn hàng nếu trạng thái là "Đã hủy"
            _context.donHangs.Remove(donHang);
            await _context.SaveChangesAsync();

            TempData["DH-Info"] = "Đơn hàng cũ đã bị xóa vì đã hủy.";

            // Kiểm tra xem người dùng đã đăng nhập
            var userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để thực hiện đơn hàng.";
                return RedirectToAction("Login", "Account");
            }

            // Tạo một đơn hàng mới với thông tin của đơn hàng cũ
            var newDonHang = new DonHang
            {
                Id = Guid.NewGuid(),
                IdKH = new Guid(userId), // Gán ID người dùng hiện tại
                NgayTao = DateTime.Now,
                TrangThaiDonHang = "Chưa xác nhận", // Trạng thái mới
                TongTien = donHang.TongTien,
                TenNguoiNhan = donHang.TenNguoiNhan,
                SoDienThoai = donHang.SoDienThoai,
                DiaChi = donHang.DiaChi,
                PhuongThucThanhToan = donHang.PhuongThucThanhToan,
                ChiTietDonHangs = donHang.ChiTietDonHangs.Select(ct => new ChiTietDonHang
                {
                    Id = Guid.NewGuid(),
                    IdSP = ct.IdSP, // Sửa theo thuộc tính có sẵn trong ChiTietDonHang
                    SoLuong = ct.SoLuong,
                }).ToList()
            };

            // Thêm đơn hàng mới vào cơ sở dữ liệu
            _context.donHangs.Add(newDonHang);
            await _context.SaveChangesAsync();

            TempData["DH-Success"] = "Đơn hàng đã được tạo lại thành công.";
            return RedirectToAction("ViewDonHang");
        }


        public async Task<IActionResult> HuyDonHang(Guid id)
        {
            // Tìm đơn hàng bằng Id
            var donHang = await _context.donHangs
                .Include(dh => dh.ChiTietDonHangs) // Bao gồm cả chi tiết đơn hàng
                    .ThenInclude(ct => ct.SanPham) // Bao gồm cả sản phẩm
                .Include(dh => dh.HoaDon) // Bao gồm cả hóa đơn liên quan
                .FirstOrDefaultAsync(dh => dh.Id == id);

            if (donHang == null)
            {
                TempData["DH-Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("ViewDonHang");
            }

            // Tính toán chi phí hoàn trả với tỷ lệ 10%
            decimal phiHoanTra = donHang.TongTien * 0.10m; // 10% chi phí hoàn trả

            if (donHang.HoaDon != null)
            {
                // Kiểm tra địa chỉ và số điện thoại có hợp lệ không
                if (string.IsNullOrWhiteSpace(donHang.DiaChi) || string.IsNullOrWhiteSpace(donHang.SoDienThoai))
                {
                    TempData["Error"] = "Thông tin địa chỉ và số điện thoại không hợp lệ. Không thể thực hiện hủy đơn hàng.";
                    return RedirectToAction("ViewDonHang");
                }

                // Cập nhật trạng thái đơn hàng và hóa đơn thành "Đã hủy"
                donHang.TrangThaiDonHang = "Đã hủy";
                donHang.HoaDon.TrangThai = false; // Cập nhật trạng thái hóa đơn thành false
                _context.hoaDons.Update(donHang.HoaDon); // Cập nhật hóa đơn

                // Cập nhật số lượng sản phẩm trong kho
                foreach (var chiTiet in donHang.ChiTietDonHangs)
                {
                    var sanPham = chiTiet.SanPham;
                    sanPham.SoLuong += chiTiet.SoLuong; // Tăng số lượng sản phẩm trong kho
                    _context.sanPhams.Update(sanPham); // Cập nhật sản phẩm
                }

                TempData["DH-Success"] = $"Đơn hàng đã được hủy. Chi phí hoàn trả là {phiHoanTra.ToString("N2")}.";
            }
            else
            {
                // Cập nhật trạng thái đơn hàng thành "Đã hủy" nếu không có hóa đơn
                donHang.TrangThaiDonHang = "Đã hủy";

                // Cập nhật số lượng sản phẩm trong kho
                foreach (var chiTiet in donHang.ChiTietDonHangs)
                {
                    var sanPham = chiTiet.SanPham;
                    sanPham.SoLuong += chiTiet.SoLuong; // Tăng số lượng sản phẩm trong kho
                    _context.sanPhams.Update(sanPham); // Cập nhật sản phẩm
                }

                TempData["DH-Success"] = "Đơn hàng đã được hủy.";
            }

            _context.donHangs.Update(donHang);
            await _context.SaveChangesAsync();

            // Chuyển hướng đến trang hiển thị thông tin trả hàng nếu có hóa đơn
            if (donHang.HoaDon != null)
            {
                return RedirectToAction("NhapThongTinTraHang", "TraHang", new { id = donHang.Id, phiHoanTra });
            }

            return RedirectToAction("ViewDonHang");
        }




    }
}
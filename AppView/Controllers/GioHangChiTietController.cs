using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppView.Controllers
{
    [Authorize(Roles = "User")] // Chỉ cho phép người dùng với vai trò "User" truy cập
    public class GioHangChiTietController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GioHangChiTietController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Xem giỏ hàng
        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _context.gioHangChiTiets
                                          .Where(ct => ct.GioHang.IdKH == userId)
                                          .Include(ct => ct.SanPham)
                                          .ToListAsync();

            return View(cartItems);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(Dictionary<Guid, int> quantities)
        {
            var userIdString = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để cập nhật giỏ hàng.";
                return RedirectToAction("Login", "Account");
            }

            foreach (var item in quantities)
            {
                var cartItem = await _context.gioHangChiTiets
                                              .FirstOrDefaultAsync(ct => ct.Id == item.Key && ct.GioHang.IdKH == userId);

                if (cartItem != null)
                {
                    var sanPham = await _context.sanPhams.FindAsync(cartItem.IdSP);
                    if (sanPham != null && sanPham.SoLuong >= item.Value)
                    {
                        sanPham.SoLuong += cartItem.SoLuong - item.Value; // Cập nhật số lượng sản phẩm trong kho
                        cartItem.SoLuong = item.Value; // Cập nhật số lượng sản phẩm trong giỏ hàng

                        _context.gioHangChiTiets.Update(cartItem);
                        _context.sanPhams.Update(sanPham);
                    }
                    else
                    {
                        TempData["Error"] = "Số lượng sản phẩm không đủ.";
                    }
                }
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Giỏ hàng đã được cập nhật.";
            return RedirectToAction("Index");
        }



        // Xóa sản phẩm khỏi giỏ hàng
        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            var cartItem = await _context.gioHangChiTiets.FirstOrDefaultAsync(c => c.Id == id);
            if (cartItem == null)
            {
                // Log hoặc thông báo nếu không tìm thấy chi tiết giỏ hàng
                TempData["Error"] = "Chi tiết giỏ hàng không tồn tại.";
                return RedirectToAction("Index");
            }

            var sanPham = await _context.sanPhams.FirstOrDefaultAsync(sp => sp.Id == cartItem.IdSP);
            if (sanPham == null)
            {
                // Log hoặc thông báo nếu không tìm thấy sản phẩm
                TempData["Error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction("Index");
            }

            // Cập nhật lại số lượng sản phẩm
            sanPham.SoLuong += cartItem.SoLuong;

            // Cập nhật trạng thái sản phẩm
            sanPham.TrangThai = sanPham.SoLuong > 0;

            // Lưu cập nhật sản phẩm
            _context.sanPhams.Update(sanPham);

            // Xóa chi tiết giỏ hàng
            _context.gioHangChiTiets.Remove(cartItem);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Sản phẩm đã được xóa khỏi giỏ hàng và số lượng đã được cập nhật.";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> DatMua()
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

            // Chuyển hướng đến trang Checkout để nhập thông tin giao hàng
            return RedirectToAction("Checkout", "ThanhToan");
        }




        private async Task<decimal> CalculateTotalAmount(List<Guid> selectedProductIds)
        {
            var products = await _context.sanPhams.Where(p => selectedProductIds.Contains(p.Id)).ToListAsync();
            return products.Sum(p => p.Gia * (_context.gioHangChiTiets.FirstOrDefault(c => c.IdSP == p.Id)?.SoLuong ?? 0));
        }


        private async Task<List<SanPham>> GetProductsByIds(List<Guid> ids)
        {
            return await _context.sanPhams.Where(p => ids.Contains(p.Id)).ToListAsync();
        }


        private async Task RemoveProductsFromCart(List<Guid> ids)
        {
            var cartItems = await _context.gioHangChiTiets.Where(c => ids.Contains(c.Id)).ToListAsync();
            _context.gioHangChiTiets.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        // Hiển thị danh sách đơn hàng
        public async Task<IActionResult> DanhSachDonHang()
        {
            var userIdString = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                TempData["Error"] = "Bạn cần đăng nhập để xem danh sách đơn hàng.";
                return RedirectToAction("Login", "Account");
            }

            var user = await _context.Users
                              .Include(u => u.DonHangs) // Nếu User có thuộc tính DonHangs
                              .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                TempData["Error"] = "Người dùng không tồn tại.";
                return RedirectToAction("Index", "Home");
            }

            var donHangs = user.DonHangs.ToList();

            return View(donHangs);
        }
    }
}

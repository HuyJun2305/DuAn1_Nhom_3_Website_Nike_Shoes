using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    [Authorize(Roles = "User")] // Chỉ cho phép người dùng với vai trò "User" truy cập

    public class ViewKhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViewKhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ProductList(int page = 1)
        {
            int pageSize = 9; // Số sản phẩm mỗi trang

            var products = await _context.sanPhams
                .Where(p => p.TrangThai) // Lọc các sản phẩm còn hàng
                .OrderBy(p => p.Ten) // Sắp xếp theo tên sản phẩm
                .Skip((page - 1) * pageSize) // Bỏ qua các sản phẩm của các trang trước
                .Take(pageSize) // Lấy số sản phẩm cho trang hiện tại
                .Select(p => new SanPhamViewModel
                {
                    Id = p.Id,
                    Ten = p.Ten,
                    Gia = p.Gia,
                    SoLuong = p.SoLuong,
                    ImgFile = p.ImgFile
                })
                .ToListAsync();

            var totalProducts = await _context.sanPhams.CountAsync(p => p.TrangThai); // Tổng số sản phẩm

            var viewModel = new SanPhamListViewModel
            {
                Products = products,
                Categories = await _context.danhMucSanPhams
                    .Select(c => new DanhMucSanPhamViewModel
                    {
                        Id = c.Id,
                        TenDM = c.TenDM
                    })
                    .ToListAsync(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize)
            };

            return View(viewModel);
        }



    }
}
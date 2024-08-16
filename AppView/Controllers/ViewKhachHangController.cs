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
        public IActionResult ProductList(string searchTerm, List<string> priceFilter, List<string> sizeFilter, Guid? categoryId)
        {
            var query = _context.sanPhams.AsQueryable();

            // Tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Ten.Contains(searchTerm));
                //query = query.Where(p => p.Ten.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Lọc theo danh mục
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.DanhMucSanPham.Id == categoryId.Value);
            }

            // Lọc theo khoảng giá
            if (priceFilter != null && priceFilter.Any())
            {
                var priceRanges = priceFilter.Select(p => p.Split('-')).ToList();
                foreach (var range in priceRanges)
                {
                    var minPrice = Convert.ToDecimal(range[0]);
                    var maxPrice = range.Length > 1 ? Convert.ToDecimal(range[1]) : decimal.MaxValue;

                    query = query.Where(p => p.Gia >= minPrice && p.Gia <= maxPrice);
                }
            }

            // Lọc theo kích cỡ
            if (sizeFilter != null && sizeFilter.Any())
            {
                var sizes = sizeFilter.Select(s => int.Parse(s)).ToList();
                query = query.Where(p => sizes.Contains(p.Size));
            }

            // Lấy tất cả sản phẩm sau khi lọc
            var products = query.Include(p => p.DanhMucSanPham).ToList();

            var model = new SanPhamListViewModel
            {
                Products = products,
                Categories = _context.danhMucSanPhams.ToList()
            };

            return View(model);
        }


    }
}
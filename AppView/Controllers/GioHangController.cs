using AppView.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    [Authorize(Roles = "User")] // Chỉ cho phép người dùng với vai trò "User" truy cập

    public class GioHangController : Controller
    {
        ApplicationDbContext context;
        public GioHangController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index() // Model Class chọn cartDetails để xem danh sách cartDetails
        {
            var data = context.gioHangs.ToList();
            return View(data);
        }
        

        
    }
}

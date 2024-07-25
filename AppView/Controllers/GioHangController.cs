using AppView.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class GioHangController : Controller
    {
        AppDbContext context;
        public GioHangController()
        {
            context = new AppDbContext();
        }
        public IActionResult Index() // Model Class chọn cartDetails để xem danh sách cartDetails
        {
            var data = context.gioHangs.ToList();
            return View(data);
        }
        

        
    }
}

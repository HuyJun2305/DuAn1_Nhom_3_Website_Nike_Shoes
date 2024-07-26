using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _context;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new AppDbContext();
        }

        public IActionResult Index()
        {
            var sessiondata = HttpContext.Session.GetString("username");
            var user =  _context.khachHangs.Find(Guid.Parse(sessiondata));
            if (sessiondata == null)
            {
                ViewData["login"] = "Chưa đăng nhập";
            }
            else ViewData["login"] = $"Xin chào {user.Username}";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

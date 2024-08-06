using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}

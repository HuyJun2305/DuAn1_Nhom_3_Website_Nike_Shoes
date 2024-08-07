using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AppView.Models;
using Microsoft.AspNetCore.Authorization;




public class HomeController : Controller
{

    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
       // Truy xuất thông tin người dùng từ session
        var userId = HttpContext.Session.GetString("userId");

        if (userId != null)
        {
            // Truy xuất thông tin người dùng từ cơ sở dữ liệu dựa trên userId
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                ViewBag.UserName = user.Ten; // Gán tên người dùng vào ViewBag
            }
        }

        return View();
    }
}

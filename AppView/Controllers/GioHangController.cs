using AppView.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult AddToBill(/*List<CartDetails> details*/)
        {
            var check = HttpContext.Session.GetString("username");
            if (String.IsNullOrEmpty(check))
            {
                return RedirectToAction("Login", "Account");// chuyen huong ve trang login
            }
            else
            {
                var CartItem = context.gioHangChiTiets.FirstOrDefault(p => p.Username == check);
                if (CartItem == null)
                {
                    return Content("Trong giỏ có gì đâu mà mua???");
                }
                else
                {
                    {

                    }
                }
            }
            return RedirectToAction("Index", "Bill");
        }
    }
}

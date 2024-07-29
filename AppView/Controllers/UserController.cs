using AppView.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AppView.Controllers
{
    public class UserController : Controller
    {
        AppDbContext _context;
        public UserController()
        {
            _context = new AppDbContext();

        }

        public ActionResult Login(string username, string password) // Đăng nhập
        {
            if (username == null && password == null)
            {
                return View();
            }
            else
            {
                var account = _context.khachHangs.FirstOrDefault(p => p.Username == username && p.Password == password);
                if (account == null) return Content("Tài khoản bạn đang đăng nhập khum tồn tại");
                else
                {
                    HttpContext.Session.SetString("username", account.Id.ToString()); // Lưu username vào session
                    return RedirectToAction("Index", "Home");
                }
            }
        }
            public ActionResult SignUp() // Tạo tài khoản - mở view để tạo
            {
                return View();
            }

            // POST: AccountController/Create
            [HttpPost]
            public ActionResult SignUp(User account) // Tạo tài khoản - Thực hiện tạo mới account
            {

                    _context.khachHangs.Add(account);
                    // Khi tạo tài khoản đồng thời tạo ra 1 Giỏ hàng tương ứng với nó
                    GioHang cart = new GioHang() { IdKH = account.Id};
                    _context.gioHangs.Add(cart);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Create account successfully!";
                    return RedirectToAction("Login", "User");
                
                
            }


        // GET: KhachHangController
        public ActionResult Index()
        {
            var data = _context.khachHangs.ToList();
            return View(data);
        }

        // GET: KhachHangController/Details/5
        public ActionResult Details(Guid id)
        {
            var data = _context.khachHangs.Find(id);
            return View(data);
        }


        public ActionResult Edit(Guid id)
        {
            var data = _context.khachHangs.Find(id);
            return View(data);
        }

        // POST: KhachHangController/Edit/5
        [HttpPost]
        public ActionResult Edit(User kh, IFormFile imgFile)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                imgFile.CopyTo(stream);
                kh.ImgUrl = imgFile.FileName;

                var editKH = _context.khachHangs.Find(kh.Id);
                editKH.Ten = kh.Ten;    
                editKH.ImgUrl = kh.ImgUrl;    
                editKH.SDT = kh.SDT;    
                editKH.Email = kh.Email;    
                editKH.TrangThai = kh.TrangThai;    
                _context.khachHangs.Update(editKH);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: KhachHangController/Delete/5
        public ActionResult Delete(Guid id)
        {
            // Tìm khách hàng cùng với giỏ hàng liên quan
            var deleteKH = _context.khachHangs
                .Include(kh => kh.GioHang)
                .FirstOrDefault(kh => kh.Id == id);

            // Kiểm tra nếu khách hàng không tồn tại
            if (deleteKH == null)
            {
                return NotFound(); // Hoặc chuyển hướng đến trang lỗi
            }

            try
            {
                // Xóa giỏ hàng nếu tồn tại
                if (deleteKH.GioHang != null)
                {
                    _context.gioHangs.Remove(deleteKH.GioHang);
                }

                // Xóa khách hàng
                _context.khachHangs.Remove(deleteKH);

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                // Log lỗi hoặc thông báo cho người dùng
                // Ví dụ: return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                return StatusCode(StatusCodes.Status500InternalServerError, "Không thể xóa khách hàng. Lỗi: " + ex.Message);
            }

            // Chuyển hướng đến trang danh sách khách hàng
            return RedirectToAction("Index");
        }





    }
}

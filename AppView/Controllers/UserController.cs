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


        public async Task<IActionResult> Edit(Guid id)
        {
            var kh = await _context.khachHangs.FindAsync(id);
            if (kh == null)
            {
                return NotFound();
            }

            return View(kh);
        }
        // POST: KhachHangController/Edit/5
        //[HttpPost]
        //public async Task<IActionResult> Edit(Guid id, User kh, IFormFile imgFile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.danhMucSanPhams = await _context.danhMucSanPhams.ToListAsync();
        //        return View(kh);
        //    }

        //    try
        //    {
        //        var editKH = await _context.khachHangs.FindAsync(id);
        //        if (editKH == null)
        //        {
        //            return NotFound("User not found.");
        //        }

        //        editKH.Ten = kh.Ten;
        //        editKH.SDT = kh.SDT;
        //        editKH.Email = kh.Email;

        //        editKH.TrangThai = kh.TrangThai;

        //        if (imgFile != null && imgFile.Length > 0)
        //        {
        //            // Lưu hình ảnh mới mà không xóa ảnh cũ
        //            var fileName = Path.GetFileName(imgFile.FileName);
        //            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await imgFile.CopyToAsync(stream);
        //            }

        //            // Cập nhật đường dẫn hình ảnh trong cơ sở dữ liệu
        //            editKH.ImgUrl = fileName;
        //        }

        //        // Cập nhật sản phẩm trong cơ sở dữ liệu
        //        _context.Update(editKH);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý lỗi và có thể ghi log hoặc trả về thông báo lỗi chi tiết
        //        return BadRequest($"Có lỗi xảy ra: {ex.Message}");
        //    }
        //}




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

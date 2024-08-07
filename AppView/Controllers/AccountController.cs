using AppView.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _roleManager = roleManager;
    }



    // Danh sách người dùng
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var model = new UserListViewModel
        {
            Users = users
        };
        return View(model);
    }





    // GET: /Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Ten = model.Ten // Đảm bảo rằng thuộc tính "Ten" không NULL
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Tạo giỏ hàng mới cho người dùng
                await CreateCartForUser(user.Id);

                // Gán vai trò cho người dùng
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Xử lý lỗi khi thêm vai trò
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                // Xử lý lỗi khi tạo người dùng
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }



    private async Task CreateCartForUser(Guid userId)
    {
        var userCart = new GioHang
        {
            Id = Guid.NewGuid(), // Đây là khóa chính của bảng GioHang
            IdKH = userId // Khóa ngoại đến bảng AspNetUsers
        };

        _context.gioHangs.Add(userCart);
        await _context.SaveChangesAsync();
    }

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            HttpContext.Session.SetString("userId", user.Id.ToString());

            TempData["WelcomeMessage"] = $"Chào mừng {user.UserName}!";

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (roles.Contains("User"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User does not have a recognized role.");
            }
        }
        else if (result.IsLockedOut)
        {
            return View("Lockout");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    private async Task EnsureRolesExistAsync()
{
    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }
}



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        HttpContext.Session.Clear(); // Xóa session khi đăng xuất

        // Chuyển hướng về trang chính
        return RedirectToAction("Login", "Account");
    }


    // Xóa người dùng
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        // Tìm người dùng bằng ID
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound(); // Nếu không tìm thấy người dùng, trả về lỗi NotFound
        }

        // Xóa tất cả các giỏ hàng của người dùng
        var userCarts = await _context.gioHangs.Where(c => c.IdKH == user.Id).ToListAsync();
        if (userCarts.Any())
        {
            _context.gioHangs.RemoveRange(userCarts);
            await _context.SaveChangesAsync();
        }

        // Xóa tất cả các đơn hàng của người dùng
        var userOrders = await _context.donHangs.Where(d => d.IdKH == user.Id).ToListAsync();
        if (userOrders.Any())
        {
            _context.donHangs.RemoveRange(userOrders);
            await _context.SaveChangesAsync();
        }

        // Xóa người dùng
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            // Nếu xóa thành công, chuyển hướng đến trang Index
            return RedirectToAction("Index");
        }

        // Nếu có lỗi khi xóa người dùng, thêm lỗi vào ModelState và chuyển hướng đến trang Index
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        // Nếu có lỗi, giữ nguyên trang và hiển thị lỗi
        return View("Index");
    }

}

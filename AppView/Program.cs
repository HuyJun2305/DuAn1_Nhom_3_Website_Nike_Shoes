using AppView.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.None; // Or SameSiteMode.Lax if not cross-site
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();


// Đăng ký AppDbContext với DI container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Đăng ký dịch vụ PdfService
builder.Services.AddTransient<PdfService>();

// Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Yêu cầu HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict; // Hoặc `Lax` tùy thuộc vào nhu cầu của bạn

});



builder.Services.AddControllersWithViews();

///

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHttpsRedirection();

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); //khai bao su dung session

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}");

app.Run();
    
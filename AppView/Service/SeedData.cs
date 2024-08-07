using AppView.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Tạo các vai trò nếu chưa tồn tại
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var role = new IdentityRole<Guid> { Name = roleName, NormalizedName = roleName.ToUpper() };
                await roleManager.CreateAsync(role);
            }
        }

        // Tạo Admin
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                Ten = "Admin User",
                SDT = "0123456789",
                LockoutEnabled = true
            };
            var createAdminUser = await userManager.CreateAsync(adminUser, "AdminPass123!");
            if (createAdminUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Tạo User
        var regularUser = await userManager.FindByEmailAsync("user@example.com");
        if (regularUser == null)
        {
            regularUser = new ApplicationUser
            {
                UserName = "user@example.com",
                Email = "user@example.com",
                Ten = "Regular User",
                SDT = "0987654321",
                LockoutEnabled = true
            };
            var createRegularUser = await userManager.CreateAsync(regularUser, "UserPass123!");
            if (createRegularUser.Succeeded)
            {
                await userManager.AddToRoleAsync(regularUser, "User");
            }
        }
    }
}

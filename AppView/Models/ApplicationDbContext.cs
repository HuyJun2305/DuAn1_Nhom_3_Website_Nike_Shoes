using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppView.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<ApplicationUser> khachHangs { get; set; }
        public DbSet<DanhMucSanPham> danhMucSanPhams { get; set; }
        public DbSet<GioHang> gioHangs { get; set; }
        public DbSet<GioHangChiTiet> gioHangChiTiets { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<DonHang> donHangs { get; set; }
        public DbSet<ChiTietDonHang> chiTietDonHangs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình liên kết giữa DonHang và KhachHang
            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.KhachHang)
                .WithMany(u => u.DonHangs)
                .HasForeignKey(d => d.IdKH)
                .OnDelete(DeleteBehavior.NoAction);

            // Cấu hình liên kết giữa DonHang và HoaDon
            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.HoaDon)
                .WithMany(h => h.DonHangs)
                .HasForeignKey(d => d.IdHD)
                .OnDelete(DeleteBehavior.NoAction);

            // Cấu hình liên kết giữa DonHang và ChiTietDonHang
            modelBuilder.Entity<DonHang>()
                .HasMany(d => d.ChiTietDonHangs)
                .WithOne(c => c.DonHang)
                .HasForeignKey(c => c.IdDH)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình liên kết giữa ChiTietDonHang và SanPham
            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(c => c.SanPham)
                .WithMany()
                .HasForeignKey(c => c.IdSP);

            // Cấu hình khóa chính cho ChiTietDonHang
            modelBuilder.Entity<ChiTietDonHang>()
                .HasKey(c => c.Id);

            // Cấu hình liên kết giữa HoaDon và KhachHang
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.KhachHang)
                .WithMany(hk => hk.HoaDons)
                .HasForeignKey(h => h.IdKH)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ một-một giữa ApplicationUser và GioHang
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.GioHang)
                .WithOne(g => g.User) // Đảm bảo GioHang có thuộc tính ApplicationUser
                .HasForeignKey<GioHang>(g => g.IdKH) // Key trong GioHang tương ứng với Id của ApplicationUser
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data cho AspNetRoles
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            var adminUserId = Guid.NewGuid();
            var regularUserId = Guid.NewGuid();

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    Ten = "Admin User",
                    SDT = "0123456789",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    PasswordHash = passwordHasher.HashPassword(null, "AdminPass123!"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = regularUserId,
                    UserName = "user@example.com",
                    NormalizedUserName = "USER@EXAMPLE.COM",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    Ten = "Regular User",
                    SDT = "0987654321",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    PasswordHash = passwordHasher.HashPassword(null, "UserPass123!"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = regularUserId,
                    RoleId = userRoleId
                }
            );
        }



    }
}


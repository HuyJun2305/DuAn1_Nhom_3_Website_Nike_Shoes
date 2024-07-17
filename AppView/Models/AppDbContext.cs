using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AppView.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<User> khachHangs { get; set; }
        public DbSet<DanhMucSanPham> danhMucSanPhams { get; set; }
        public DbSet<GioHang> gioHangs { get; set; }
        public DbSet<GioHangChiTiet> gioHangChiTiets { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=HUYJUN\\SQLEXPRESS;Initial Catalog=Nhom_10_AppDbContext;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

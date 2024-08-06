using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class DonHang
    {
        public Guid Id { get; set; }
        public Guid IdKH { get; set; } // Khóa ngoại đến bảng AspNetUsers
        public Guid? IdHD { get; set; } // Khóa ngoại đến bảng HoaDon
        public DateTime NgayTao { get; set; }
        public string TrangThaiDonHang { get; set; }
        public decimal TongTien { get; set; }
        public string TenNguoiNhan { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string PhuongThucThanhToan { get; set; }

        // Quan hệ đến bảng AspNetUsers
        [ForeignKey("IdKH")]
        public ApplicationUser KhachHang { get; set; }

        // Quan hệ đến bảng HoaDon
        [ForeignKey("IdHD")]
        public virtual HoaDon HoaDon { get; set; }

        // Quan hệ nhiều-nhiều với ChiTietDonHang
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        // Quan hệ nhiều-nhiều với HoaDonChiTiet
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }

    }
}

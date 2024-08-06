using AppView.Models;

namespace AppView.ViewModels
{
    public class HoaDonChiTietViewModel
    {
        public Guid HoaDonId { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal TongTien { get; set; }
        public bool TrangThai { get; set; }
        public string KhachHangTen { get; set; }
        public string KhachHangEmail { get; set; }
        public string SanPhamTen { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        // Thêm thuộc tính HoaDon nếu cần
        public HoaDon HoaDon { get; set; }
        public ICollection<DonHangViewModel> DonHangs { get; set; } // Đơn hàng
        public ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } // HĐCT
        public virtual ApplicationUser KhachHang { get; set; } //Khách hàng



    }
}

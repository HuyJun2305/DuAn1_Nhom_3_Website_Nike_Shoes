using AppView.Models;

namespace AppView.ViewModels
{
    public class HoaDonChiTietViewModel
    {
        public HoaDon HoaDon { get; set; }
        public User KhachHang { get; set; }
        public List<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public List<SanPham> SanPhams { get; set; }

    }
}

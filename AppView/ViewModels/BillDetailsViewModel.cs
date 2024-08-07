using AppView.Models;

namespace AppView.ViewModels
{
    public class BillDetails
    {
        public HoaDon HoaDon { get; set; }
        public List<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public List<SanPham> SanPhams { get; set; }
    }
}

using AppView.Models;

namespace AppView.ViewModels
{
    public class HoaDonViewModel
    {
        public HoaDon HoaDon { get; set; }
        public List<GioHangChiTiet> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
    }

}

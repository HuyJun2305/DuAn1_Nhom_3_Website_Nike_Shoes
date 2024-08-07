using AppView.Models;

namespace AppView.ViewModels
{
    public class ThanhToanViewModel
    {
        public decimal TotalAmount { get; set; }
        public Guid[] SelectedProductIds { get; set; }
        public DonHang DonHang { get; set; }

        public IEnumerable<GioHangChiTiet> CartItems { get; set; } = new List<GioHangChiTiet>();
        public string DiaChi { get; set; } = string.Empty;
        public string PhuongThucThanhToan { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;

    }

}

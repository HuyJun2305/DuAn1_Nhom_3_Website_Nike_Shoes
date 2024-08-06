namespace AppView.ViewModels
{
    public class DonHangViewModel
    {
        public Guid DonHangId { get; set; }
        public string SanPhamTen { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayTao { get; set; }
        public string TrangThaiDonHang { get; set; }
        public string TenNguoiNhan { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string PhuongThucThanhToan { get; set; }
    }
}

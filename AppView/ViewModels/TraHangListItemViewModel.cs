namespace AppView.ViewModels
{
    public class TraHangListItemViewModel
    {
        public Guid Id { get; set; }
        public Guid IdDonHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string GhiChu { get; set; }
        public decimal PhiHoanTra { get; set; }
        public DateTime NgayTraHang { get; set; }
        public string TrangThaiDonHang { get; set; }
        public string TrangThaiTraHang { get; set; }
    }
}

namespace AppView.Models
{
    public class HoaDonChiTiet
    {
        public Guid Id { get; set; }
        public double? Gia { get; set; }
        public int? SoLuong { get; set; }
        public Guid? IdSP { get; set; }
        public virtual SanPham? SanPhams { get; set; }
        public Guid? IdHD { get; set; }
        public virtual HoaDon? HoaDons { get; set; }
        public Guid? IdTT { get; set; }
        public virtual ThanhToan? ThanhToans { get; set; }
    }
}

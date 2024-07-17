namespace AppView.Models
{
    public class ThanhToan
    {
        public Guid Id { get; set; }
        public string? PhuongThuc { get; set; }
        public DateTime? NgayTao { get; set; }
        public decimal? TongTien { get; set; }
        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; } 
    }
}

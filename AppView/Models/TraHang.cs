using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class TraHang
    {
        public Guid Id { get; set; }
        public Guid IdDH { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string GhiChu { get; set; }
        public decimal PhiHoanTra { get; set; }
        public DateTime NgayTraHang { get; set; }
        public string TrangThaiTraHang { get; set; }

        // Khóa ngoại liên kết với đơn hàng
        [ForeignKey("IdDH")]
        public DonHang DonHang { get; set; }

        public ICollection<ChiTietTraHang> ChiTietTraHangs { get; set; }
    }
}

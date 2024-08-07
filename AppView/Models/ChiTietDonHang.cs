using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdDH { get; set; }  // Foreign key to DonHang
        [ForeignKey("IdDH")]
        public virtual DonHang DonHang { get; set; }

        public Guid IdSP { get; set; }  // Foreign key to SanPham
        [ForeignKey("IdSP")]
        public virtual SanPham SanPham { get; set; }

        public int SoLuong { get; set; }

        public decimal Gia { get; set; }  // Giá tại thời điểm mua
    }

}

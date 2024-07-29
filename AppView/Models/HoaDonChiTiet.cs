using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class HoaDonChiTiet
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public Guid IdSP { get; set; }
        [ForeignKey("IdSP")]

        public virtual SanPham SanPham { get; set; }

        public Guid IdHD { get; set; }
        [ForeignKey("IdHD")]
        public virtual HoaDon HoaDon { get; set; }
        public Guid? IdTT { get; set; }
        public virtual ThanhToan? ThanhToans { get; set; }
    }
}

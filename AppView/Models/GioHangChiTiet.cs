using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class GioHangChiTiet
    {
        [Key]
        public Guid Id { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }

        public Guid IdSP { get; set; }
        [ForeignKey("IdSP")]
        public virtual SanPham SanPham { get; set; }


        public Guid IdGH { get; set; }
        [ForeignKey("IdGH")]
        public virtual GioHang GioHang { get; set; }

    }
}

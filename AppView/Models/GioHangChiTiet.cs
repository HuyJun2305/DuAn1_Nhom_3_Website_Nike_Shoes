using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class GioHangChiTiet
    {
        [Key]
        public Guid Id { get; set; }
        public int SoLuong { get; set; }


        [ForeignKey("SanPham")]
        public Guid IdSP { get; set; }
        public virtual SanPham SanPham { get; set; }


        [ForeignKey("GioHang")]
        public Guid IdGH { get; set; }
        public virtual GioHang GioHang { get; set; }

    }
}

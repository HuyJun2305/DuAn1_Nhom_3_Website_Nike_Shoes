using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class HoaDonChiTiet
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdSP { get; set; }
        [ForeignKey("IdSP")]

        public virtual SanPham SanPham { get; set; }

        public Guid IdHD { get; set; }
        [ForeignKey("IdHD")]
        public virtual HoaDon HoaDon { get; set; }
    }
}

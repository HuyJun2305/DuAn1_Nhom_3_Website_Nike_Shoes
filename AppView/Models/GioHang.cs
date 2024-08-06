using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class GioHang
    {
        [Key]
        public Guid Id { get; set; }
        public decimal? TongTien { get; set; }
        public Guid IdKH { get; set; }
        [ForeignKey("IdKH")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiet { get; set; }

    }
}

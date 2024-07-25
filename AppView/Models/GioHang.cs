using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class GioHang
    {
        [Key]
        public Guid Id { get; set; }
        public decimal? TongTien { get; set; }


        [ForeignKey("User")]
        public Guid IdKH { get; set; }


        public virtual User User { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiet { get; set; }

    }
}

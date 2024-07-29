using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class TodayDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dateValue = (DateTime?)value;
            return dateValue.HasValue && dateValue.Value.Date == DateTime.Today.Date;
        }
    }
    public class HoaDon
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public decimal TongTien { get; set; }
        public bool TrangThai { get; set; }
        [TodayDate]
        public DateTime NgayTao { get; set; }

        [ForeignKey("KhachHang")]
        public Guid IdKH { get; set; }
        public virtual User KhachHang { get; set; }
        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; }

    }
}

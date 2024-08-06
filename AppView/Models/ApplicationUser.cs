using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppView.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string Ten { get; set; }

        public string? ImgUrl { get; set; }

        [RegularExpression("^((\\+84)|0)[0-9]{8,11}$", ErrorMessage = "Số điện thoại phải đúng format và có 10 chữ số")]
        public string? SDT { get; set; }

        public bool? TrangThai { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public ICollection<DonHang> DonHangs { get; set; }

        // Thay đổi kiểu quan hệ để phản ánh rằng mỗi người dùng có một giỏ hàng
        public virtual GioHang GioHang { get; set; }
    }
}




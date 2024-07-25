using System.ComponentModel.DataAnnotations;

namespace AppView.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Ten { get; set; }
        public string? ImgUrl { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "UserName phải có tối thiểu 10 ký tự")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [RegularExpression("^((\\+84)|0)[0-9]{8,11}$", ErrorMessage = "Số điện thoại phải đúng format và có 10 chữ số")]
        public string SDT { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool TrangThai { get; set; }
        public virtual List<HoaDon>? HoaDons { get; set; }
        public virtual GioHang? GioHang { get; set; }
    }
}

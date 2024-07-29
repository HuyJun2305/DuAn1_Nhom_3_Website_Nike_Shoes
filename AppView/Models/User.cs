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
        [StringLength(50, MinimumLength = 10, ErrorMessage = "UserName phải có tối thiểu 10 ký tự")]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
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

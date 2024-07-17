using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AppView.Models
{
    public class NhanVien
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Ten { get; set; }
        public string ImgUrl { get; set; }
        [StringLength(10, MinimumLength = 10,ErrorMessage ="UserName phải có tối thiểu 10 ký tự")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string SDT { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Roles { get; set; }

        public bool TrangThai { get; set; }

    }
    
}

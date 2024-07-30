using System.ComponentModel.DataAnnotations;

namespace AppView.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "UserName phải có tối thiểu 10 ký tự")]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
    }
}

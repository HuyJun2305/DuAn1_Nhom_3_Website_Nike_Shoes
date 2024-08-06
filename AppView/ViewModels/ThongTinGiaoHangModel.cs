using System.ComponentModel.DataAnnotations;

namespace AppView.ViewModels
{
    public class ThongTinGiaoHangModel
    {
        [Required]
        public string TenNguoiNhan { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PaymentMethod { get; set; }
    }
}

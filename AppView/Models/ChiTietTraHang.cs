using System.ComponentModel.DataAnnotations.Schema;

namespace AppView.Models
{
    public class ChiTietTraHang
    {
        public Guid Id { get; set; }
        public Guid IdTraHang { get; set; }
        public string NguoiXuLy { get; set; } // Thông tin về người xử lý
        public DateTime NgayXacNhan { get; set; }

        // Khóa ngoại liên kết với trả hàng
        [ForeignKey("IdTraHang")]
        public TraHang TraHang { get; set; }
    }
}

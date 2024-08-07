namespace AppView.ViewModels
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; } // ID của sản phẩm
        public string ProductName { get; set; } // Tên sản phẩm
        public decimal Price { get; set; } // Giá sản phẩm
        public int Quantity { get; set; } // Số lượng sản phẩm
        public decimal TotalPrice => Price * Quantity; // Tổng giá cho sản phẩm
    }
}

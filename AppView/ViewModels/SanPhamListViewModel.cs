using AppView.Models;

namespace AppView.ViewModels
{
    public class SanPhamListViewModel
    {
        public IEnumerable<SanPham> Products { get; set; }
        public IEnumerable<DanhMucSanPham> Categories { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<string> PriceFilters { get; set; }
        public List<int> SizeFilters { get; set; }
    }

}

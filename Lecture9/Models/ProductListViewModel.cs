namespace Lecture9.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalValue { get; set; }
    }
}
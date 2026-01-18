namespace WebMVC.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string TrackingCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}
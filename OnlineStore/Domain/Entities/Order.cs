namespace Domain.Entities
{
    public class Order
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
        public string Status { get; set; } = "Pending";
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
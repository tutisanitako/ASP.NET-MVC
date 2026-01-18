namespace Application.DTOs
{
    public class OrderDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
        public int? UserId { get; set; }
    }
}
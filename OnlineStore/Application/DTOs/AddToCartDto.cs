namespace Application.DTOs
{
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string SessionId { get; set; } = string.Empty;
    }
}
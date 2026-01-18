namespace Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
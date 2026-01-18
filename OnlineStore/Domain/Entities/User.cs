namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
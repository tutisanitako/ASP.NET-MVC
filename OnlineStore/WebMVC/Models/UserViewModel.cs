namespace WebMVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
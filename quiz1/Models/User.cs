using System.ComponentModel.DataAnnotations;

namespace quiz1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(10, ErrorMessage = "Password must be at least 10 characters")]
        public string Password { get; set; }

        public bool IsFirstLogin { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
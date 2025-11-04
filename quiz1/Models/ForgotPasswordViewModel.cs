using System.ComponentModel.DataAnnotations;

namespace quiz1.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Your email address")]
        public string Email { get; set; }
    }
}
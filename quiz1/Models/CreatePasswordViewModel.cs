using System.ComponentModel.DataAnnotations;

namespace quiz1.Models
{
    public class CreatePasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(10, ErrorMessage = "Use a minimum of 10 characters, including letters, lowercase letters, and numbers.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{10,}$",
            ErrorMessage = "Password must contain uppercase, lowercase letters and numbers")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
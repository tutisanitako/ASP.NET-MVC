using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200)]
        [Display(Name = "Shipping Address")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;
    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Lecture10.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public bool IsAdmin { get; set; }
        public bool Has2FA { get; set; }

        [Display(Name = "Product Image")]
        public string ImagePath { get; set; }

        // ✅ NEW: Property to handle file upload (not stored in database)
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
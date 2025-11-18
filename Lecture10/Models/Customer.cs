// Models/Customer.cs
using Microsoft.AspNetCore.Http; // Add this
using System.ComponentModel.DataAnnotations;

namespace Lecture10.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required] public string Code { get; set; }
        [Required] public string Address { get; set; }
        [Required][Phone] public string Phone { get; set; }

        public bool IsAdmin { get; set; } = true;
        public bool Has2FA { get; set; } = true;

        // This stores the filename like "my-laptop.jpg"
        public string ImagePath { get; set; }

        // This is for file upload — NOT saved to DB
        [Display(Name = "Upload Product Image")]
        public IFormFile ImageFile { get; set; }
    }
}
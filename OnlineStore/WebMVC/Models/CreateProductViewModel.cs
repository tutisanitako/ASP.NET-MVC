using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, 99999, ErrorMessage = "Stock must be between 0 and 99999")]
        [Display(Name = "Stock Quantity")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50)]
        [Display(Name = "Category")]
        public string Category { get; set; } = string.Empty;
    }
}
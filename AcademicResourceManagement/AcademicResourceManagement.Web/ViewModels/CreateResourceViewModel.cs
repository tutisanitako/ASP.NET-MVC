using AcademicResourceManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AcademicResourceManagement.Web.ViewModels
{
    public class CreateResourceViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Resource type is required")]
        public ResourceType ResourceType { get; set; }

        [Required(ErrorMessage = "Please upload a file")]
        public IFormFile? File { get; set; }

        public List<SelectListItem> ResourceTypes { get; set; } = new();
    }
}
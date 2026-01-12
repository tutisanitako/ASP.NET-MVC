using AcademicResourceManagement.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AcademicResourceManagement.Application.DTOs
{
    public class CreateResourceDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ResourceType ResourceType { get; set; }
        public IFormFile? File { get; set; }
    }
}
using AcademicResourceManagement.Domain.Enums;

namespace AcademicResourceManagement.Application.DTOs
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ResourceType ResourceType { get; set; }
        public ResourceStatus Status { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string UploadedByName { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? RejectionReason { get; set; }
    }
}
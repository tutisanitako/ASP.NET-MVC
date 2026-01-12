using AcademicResourceManagement.Domain.Enums;

namespace AcademicResourceManagement.Domain.Entities
{
    public class LearningResource
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ResourceType ResourceType { get; set; }
        public ResourceStatus Status { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string UploadedByUserId { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
        public string? ApprovedByUserId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? RejectionReason { get; set; }

        // Navigation properties
        public ApplicationUser UploadedBy { get; set; } = null!;
        public ApplicationUser? ApprovedBy { get; set; }
        public ICollection<ResourceLog> Logs { get; set; } = new List<ResourceLog>();
    }
}
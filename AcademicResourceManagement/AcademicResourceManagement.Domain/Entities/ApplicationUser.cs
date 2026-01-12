using Microsoft.AspNetCore.Identity;

namespace AcademicResourceManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public ICollection<LearningResource> UploadedResources { get; set; } = new List<LearningResource>();
        public ICollection<LearningResource> ApprovedResources { get; set; } = new List<LearningResource>();
        public ICollection<ResourceLog> PerformedLogs { get; set; } = new List<ResourceLog>();
    }
}
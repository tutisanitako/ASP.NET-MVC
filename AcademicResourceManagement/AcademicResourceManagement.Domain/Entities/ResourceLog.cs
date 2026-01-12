namespace AcademicResourceManagement.Domain.Entities
{
    public class ResourceLog
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string PerformedByUserId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
        public string? Notes { get; set; }

        // Navigation properties
        public LearningResource Resource { get; set; } = null!;
        public ApplicationUser PerformedBy { get; set; } = null!;
    }
}
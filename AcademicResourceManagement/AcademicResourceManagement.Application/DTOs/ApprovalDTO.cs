namespace AcademicResourceManagement.Application.DTOs
{
    public class ApprovalDTO
    {
        public int ResourceId { get; set; }
        public bool IsApproved { get; set; }
        public string? RejectionReason { get; set; }
    }
}
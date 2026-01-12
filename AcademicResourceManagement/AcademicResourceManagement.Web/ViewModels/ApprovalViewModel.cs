using System.ComponentModel.DataAnnotations;

namespace AcademicResourceManagement.Web.ViewModels
{
    public class ApprovalViewModel
    {
        public int ResourceId { get; set; }
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Rejection reason cannot exceed 500 characters")]
        public string? RejectionReason { get; set; }
    }
}
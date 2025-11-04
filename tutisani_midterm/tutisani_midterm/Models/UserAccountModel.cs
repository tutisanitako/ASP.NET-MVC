namespace tutisani_midterm.Models
{
    public class UserAccountModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string Disability { get; set; }
        public string AccessibilityNeeds { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }
    }
}
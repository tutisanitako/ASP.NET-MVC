using System.ComponentModel.DataAnnotations;

namespace Lecture3.Models
{
    public class StudentDetailsViewModel
    {
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; } = default!;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = default!;

        [Display(Name = "Date of Birth")]
        public DateOnly? BirthDate { get; set; }

        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Year of Study")]
        public int YearOfStudy { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = default!;

        [Display(Name = "GPA (Grade Point Average)")]
        public double? GPA { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateOnly? EnrollmentDate { get; set; }
    }
}

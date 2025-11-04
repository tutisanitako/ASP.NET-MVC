using System.ComponentModel.DataAnnotations;

namespace Lecture3.Models
{
    public class StudentCreateViewModel
    {
        [Required, StringLength(11), MinLength(11)]
        [Display(Name = "Student Number")]
        public string studentNumber { get; set; } = default!;

        [Required, StringLength(60)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;

        [Required, StringLength(60)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = default!;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateOnly? BirthDate { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Range(1, 6)]
        [Display(Name = "Year of Study")]
        public int YearOfStudy { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = default!;

        [Range(0, 4)]
        [Display(Name = "GPA (Grade Point Average)")]
        public double? GPA { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateOnly? EnrollmentDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Lecture3.Models
{
    public class PersonCreateViewModel
    {
        [Required, StringLength(11), MinLength(11)]
        [Display(Name = "PersonalNumber")]
        public string personalNumber { get; set; } = default!;

        [Required, StringLength(60)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;
        [Required, StringLength(60)]
        public string LastName { get; set; } = default!;

        [DataType(DataType.Date)]
        [Display(Name = "Birthda Date")]
        public DateOnly? BitheDate { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Range(500,10000)]
        public decimal Salary { get; set; }




    }
}

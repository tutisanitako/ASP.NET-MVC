namespace Homework3.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ParentName { get; set; }
        public string City { get; set; }
        public string Grade { get; set; }
        public string StudentId { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
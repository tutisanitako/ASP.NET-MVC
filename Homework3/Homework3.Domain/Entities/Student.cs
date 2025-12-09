namespace Homework3.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ParentName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}
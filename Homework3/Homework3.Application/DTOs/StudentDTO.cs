namespace Homework3.Application.DTOs
{
    public class StudentDto
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
    }

    public class CreateStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ParentName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
    }

    public class UpdateStudentDto
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
    }

    public class PaginatedStudentsDto
    {
        public IEnumerable<StudentDto> Students { get; set; } = new List<StudentDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
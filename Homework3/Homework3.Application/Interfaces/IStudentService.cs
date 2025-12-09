using Homework3.Application.DTOs;

namespace Homework3.Application.Interfaces
{
    public interface IStudentService
    {
        Task<PaginatedStudentsDto> GetStudentsAsync(string? filter, string? orderBy, int pageNumber, int pageSize);
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);
        Task<StudentDto> UpdateStudentAsync(UpdateStudentDto dto);
        Task<bool> DeleteStudentAsync(int id);
    }
}
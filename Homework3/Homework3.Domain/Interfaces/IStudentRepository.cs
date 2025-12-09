using Homework3.Domain.Entities;

namespace Homework3.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<IEnumerable<Student>> GetFilteredAndSortedAsync(string? filter, string? orderBy, int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync(string? filter);
        Task<Student?> GetByIdAsync(int id);
        Task<Student> CreateAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
    }
}
using Homework3.Models;

namespace Homework3.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<IEnumerable<Student>> GetFilteredAndSortedAsync(string filter, string orderBy, int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync(string filter);
        Task<Student> GetByIdAsync(int id);
        Task<Student> CreateAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
    }
}
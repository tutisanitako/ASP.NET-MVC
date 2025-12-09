using Homework3.Models;

namespace Homework3.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<IEnumerable<Student>> GetFilteredAndSortedAsync(string filter, string orderBy, int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync(string filter);
    }
}
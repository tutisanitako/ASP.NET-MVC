using AcademicResourceManagement.Domain.Entities;

namespace AcademicResourceManagement.Application.Interfaces
{
    public interface IResourceLogRepository : IRepository<ResourceLog>
    {
        Task<IEnumerable<ResourceLog>> GetLogsByResourceIdAsync(int resourceId);
        Task<IEnumerable<ResourceLog>> GetLogsByUserAsync(string userId);
        Task<IEnumerable<ResourceLog>> GetRecentLogsAsync(int count);
    }
}
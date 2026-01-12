using AcademicResourceManagement.Domain.Entities;

namespace AcademicResourceManagement.Application.Interfaces
{
    public interface ILoggingService
    {
        Task LogActionAsync(int resourceId, string action, string userId, string? oldStatus = null, string? newStatus = null, string? notes = null);
        Task<IEnumerable<ResourceLog>> GetResourceLogsAsync(int resourceId);
        Task<IEnumerable<ResourceLog>> GetUserLogsAsync(string userId);
        Task<IEnumerable<ResourceLog>> GetRecentLogsAsync(int count = 50);
    }
}
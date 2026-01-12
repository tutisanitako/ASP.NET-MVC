using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Domain.Entities;

namespace AcademicResourceManagement.Application.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoggingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LogActionAsync(int resourceId, string action, string userId,
            string? oldStatus = null, string? newStatus = null, string? notes = null)
        {
            var log = new ResourceLog
            {
                ResourceId = resourceId,
                Action = action,
                PerformedByUserId = userId,
                Timestamp = DateTime.UtcNow,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                Notes = notes
            };

            await _unitOfWork.ResourceLogs.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResourceLog>> GetResourceLogsAsync(int resourceId)
        {
            return await _unitOfWork.ResourceLogs.GetLogsByResourceIdAsync(resourceId);
        }

        public async Task<IEnumerable<ResourceLog>> GetUserLogsAsync(string userId)
        {
            return await _unitOfWork.ResourceLogs.GetLogsByUserAsync(userId);
        }

        public async Task<IEnumerable<ResourceLog>> GetRecentLogsAsync(int count = 50)
        {
            return await _unitOfWork.ResourceLogs.GetRecentLogsAsync(count);
        }
    }
}
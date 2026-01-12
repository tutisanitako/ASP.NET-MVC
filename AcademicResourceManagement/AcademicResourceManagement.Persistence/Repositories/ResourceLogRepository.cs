using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Domain.Entities;
using AcademicResourceManagement.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicResourceManagement.Persistence.Repositories
{
    public class ResourceLogRepository : Repository<ResourceLog>, IResourceLogRepository
    {
        public ResourceLogRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ResourceLog>> GetLogsByResourceIdAsync(int resourceId)
        {
            return await _dbSet
                .Include(l => l.PerformedBy)
                .Where(l => l.ResourceId == resourceId)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResourceLog>> GetLogsByUserAsync(string userId)
        {
            return await _dbSet
                .Include(l => l.Resource)
                .Include(l => l.PerformedBy)
                .Where(l => l.PerformedByUserId == userId)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResourceLog>> GetRecentLogsAsync(int count)
        {
            return await _dbSet
                .Include(l => l.Resource)
                .Include(l => l.PerformedBy)
                .OrderByDescending(l => l.Timestamp)
                .Take(count)
                .ToListAsync();
        }
    }
}
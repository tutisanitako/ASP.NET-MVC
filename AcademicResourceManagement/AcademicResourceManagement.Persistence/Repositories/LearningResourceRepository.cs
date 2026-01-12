using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Domain.Entities;
using AcademicResourceManagement.Domain.Enums;
using AcademicResourceManagement.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicResourceManagement.Persistence.Repositories
{
    public class LearningResourceRepository : Repository<LearningResource>, ILearningResourceRepository
    {
        public LearningResourceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LearningResource>> GetResourcesByStatusAsync(ResourceStatus status)
        {
            return await _dbSet
                .Include(r => r.UploadedBy)
                .Include(r => r.ApprovedBy)
                .Where(r => r.Status == status)
                .OrderByDescending(r => r.UploadedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<LearningResource>> GetResourcesByUserAsync(string userId)
        {
            return await _dbSet
                .Include(r => r.ApprovedBy)
                .Where(r => r.UploadedByUserId == userId)
                .OrderByDescending(r => r.UploadedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<LearningResource>> GetApprovedResourcesAsync()
        {
            return await _dbSet
                .Include(r => r.UploadedBy)
                .Include(r => r.ApprovedBy)
                .Where(r => r.Status == ResourceStatus.Published || r.Status == ResourceStatus.Approved)
                .OrderByDescending(r => r.UploadedDate)
                .ToListAsync();
        }

        public async Task<LearningResource?> GetResourceWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(r => r.UploadedBy)
                .Include(r => r.ApprovedBy)
                .Include(r => r.Logs)
                    .ThenInclude(l => l.PerformedBy)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
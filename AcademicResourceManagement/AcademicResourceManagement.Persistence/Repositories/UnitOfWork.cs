using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Persistence.Data;

namespace AcademicResourceManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ILearningResourceRepository? _learningResources;
        private IResourceLogRepository? _resourceLogs;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ILearningResourceRepository LearningResources
        {
            get
            {
                return _learningResources ??= new LearningResourceRepository(_context);
            }
        }

        public IResourceLogRepository ResourceLogs
        {
            get
            {
                return _resourceLogs ??= new ResourceLogRepository(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
namespace AcademicResourceManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILearningResourceRepository LearningResources { get; }
        IResourceLogRepository ResourceLogs { get; }
        Task<int> SaveChangesAsync();
    }
}
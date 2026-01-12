using AcademicResourceManagement.Domain.Entities;
using AcademicResourceManagement.Domain.Enums;

namespace AcademicResourceManagement.Application.Interfaces
{
    public interface ILearningResourceRepository : IRepository<LearningResource>
    {
        Task<IEnumerable<LearningResource>> GetResourcesByStatusAsync(ResourceStatus status);
        Task<IEnumerable<LearningResource>> GetResourcesByUserAsync(string userId);
        Task<IEnumerable<LearningResource>> GetApprovedResourcesAsync();
        Task<LearningResource?> GetResourceWithDetailsAsync(int id);
    }
}
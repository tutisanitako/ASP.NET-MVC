using AcademicResourceManagement.Application.DTOs;
using AcademicResourceManagement.Domain.Enums;

namespace AcademicResourceManagement.Application.Interfaces
{
    // Service Layer Pattern Interface
    public interface IResourceService
    {
        Task<ResourceDTO?> GetResourceByIdAsync(int id);
        Task<IEnumerable<ResourceDTO>> GetAllResourcesAsync();
        Task<IEnumerable<ResourceDTO>> GetResourcesByStatusAsync(ResourceStatus status);
        Task<IEnumerable<ResourceDTO>> GetMyResourcesAsync(string userId);
        Task<IEnumerable<ResourceDTO>> GetApprovedResourcesAsync();
        Task<ResourceDTO> CreateResourceAsync(CreateResourceDTO dto, string userId);
        Task<bool> SubmitForApprovalAsync(int resourceId, string userId);
        Task<bool> ApproveResourceAsync(int resourceId, string adminId);
        Task<bool> RejectResourceAsync(int resourceId, string adminId, string reason);
        Task<bool> PublishResourceAsync(int resourceId, string adminId);
        Task<bool> DeleteResourceAsync(int resourceId, string userId);
    }
}
using AcademicResourceManagement.Application.DTOs;
using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Domain.Entities;
using AcademicResourceManagement.Domain.Enums;

namespace AcademicResourceManagement.Application.Services
{
    // Service Layer Pattern Implementation
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggingService _loggingService;
        private readonly IFileService _fileService;

        // Dependency Injection
        public ResourceService(
            IUnitOfWork unitOfWork,
            ILoggingService loggingService,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _loggingService = loggingService;
            _fileService = fileService;
        }

        public async Task<ResourceDTO?> GetResourceByIdAsync(int id)
        {
            var resource = await _unitOfWork.LearningResources.GetResourceWithDetailsAsync(id);
            if (resource == null) return null;

            return MapToDTO(resource);
        }

        public async Task<IEnumerable<ResourceDTO>> GetAllResourcesAsync()
        {
            var resources = await _unitOfWork.LearningResources.GetAllAsync();
            return resources.Select(MapToDTO);
        }

        public async Task<IEnumerable<ResourceDTO>> GetResourcesByStatusAsync(ResourceStatus status)
        {
            var resources = await _unitOfWork.LearningResources.GetResourcesByStatusAsync(status);
            return resources.Select(MapToDTO);
        }

        public async Task<IEnumerable<ResourceDTO>> GetMyResourcesAsync(string userId)
        {
            var resources = await _unitOfWork.LearningResources.GetResourcesByUserAsync(userId);
            return resources.Select(MapToDTO);
        }

        public async Task<IEnumerable<ResourceDTO>> GetApprovedResourcesAsync()
        {
            var resources = await _unitOfWork.LearningResources.GetApprovedResourcesAsync();
            return resources.Select(MapToDTO);
        }

        public async Task<ResourceDTO> CreateResourceAsync(CreateResourceDTO dto, string userId)
        {
            string filePath = string.Empty;

            if (dto.File != null)
            {
                filePath = await _fileService.SaveFileAsync(dto.File, "resources");
            }

            var resource = new LearningResource
            {
                Title = dto.Title,
                Description = dto.Description,
                ResourceType = dto.ResourceType,
                Status = ResourceStatus.Draft,
                FilePath = filePath,
                UploadedByUserId = userId,
                UploadedDate = DateTime.UtcNow
            };

            await _unitOfWork.LearningResources.AddAsync(resource);
            await _unitOfWork.SaveChangesAsync();

            await _loggingService.LogActionAsync(
                resource.Id,
                "Resource Created",
                userId,
                null,
                ResourceStatus.Draft.ToString(),
                $"Resource '{resource.Title}' created as draft"
            );

            return MapToDTO(resource);
        }

        public async Task<bool> SubmitForApprovalAsync(int resourceId, string userId)
        {
            var resource = await _unitOfWork.LearningResources.GetByIdAsync(resourceId);
            if (resource == null || resource.UploadedByUserId != userId)
                return false;

            // Business Rule: Can only submit Draft resources
            if (resource.Status != ResourceStatus.Draft)
                return false;

            var oldStatus = resource.Status.ToString();
            resource.Status = ResourceStatus.PendingApproval;

            await _unitOfWork.LearningResources.UpdateAsync(resource);
            await _unitOfWork.SaveChangesAsync();

            await _loggingService.LogActionAsync(
                resourceId,
                "Submitted for Approval",
                userId,
                oldStatus,
                resource.Status.ToString(),
                "Resource submitted for administrator review"
            );

            return true;
        }

        public async Task<bool> ApproveResourceAsync(int resourceId, string adminId)
        {
            var resource = await _unitOfWork.LearningResources.GetByIdAsync(resourceId);
            if (resource == null)
                return false;

            // Business Rule: Can only approve PendingApproval resources
            if (resource.Status != ResourceStatus.PendingApproval)
                return false;

            var oldStatus = resource.Status.ToString();
            resource.Status = ResourceStatus.Approved;
            resource.ApprovedByUserId = adminId;
            resource.ApprovedDate = DateTime.UtcNow;
            resource.RejectionReason = null;

            await _unitOfWork.LearningResources.UpdateAsync(resource);
            await _unitOfWork.SaveChangesAsync();

            await _loggingService.LogActionAsync(
                resourceId,
                "Resource Approved",
                adminId,
                oldStatus,
                resource.Status.ToString(),
                "Resource approved by administrator"
            );

            return true;
        }

        public async Task<bool> RejectResourceAsync(int resourceId, string adminId, string reason)
        {
            var resource = await _unitOfWork.LearningResources.GetByIdAsync(resourceId);
            if (resource == null)
                return false;

            // Business Rule: Can only reject PendingApproval resources
            if (resource.Status != ResourceStatus.PendingApproval)
                return false;

            var oldStatus = resource.Status.ToString();
            resource.Status = ResourceStatus.Rejected;
            resource.RejectionReason = reason;
            resource.ApprovedByUserId = adminId;
            resource.ApprovedDate = DateTime.UtcNow;

            await _unitOfWork.LearningResources.UpdateAsync(resource);
            await _unitOfWork.SaveChangesAsync();

            await _loggingService.LogActionAsync(
                resourceId,
                "Resource Rejected",
                adminId,
                oldStatus,
                resource.Status.ToString(),
                $"Resource rejected: {reason}"
            );

            return true;
        }

        public async Task<bool> PublishResourceAsync(int resourceId, string adminId)
        {
            var resource = await _unitOfWork.LearningResources.GetByIdAsync(resourceId);
            if (resource == null)
                return false;

            // Business Rule: Can only publish Approved resources
            if (resource.Status != ResourceStatus.Approved)
                return false;

            var oldStatus = resource.Status.ToString();
            resource.Status = ResourceStatus.Published;

            await _unitOfWork.LearningResources.UpdateAsync(resource);
            await _unitOfWork.SaveChangesAsync();

            await _loggingService.LogActionAsync(
                resourceId,
                "Resource Published",
                adminId,
                oldStatus,
                resource.Status.ToString(),
                "Resource made available to students"
            );

            return true;
        }

        public async Task<bool> DeleteResourceAsync(int resourceId, string userId)
        {
            var resource = await _unitOfWork.LearningResources.GetByIdAsync(resourceId);
            if (resource == null)
                return false;

            // Delete file if exists
            if (!string.IsNullOrEmpty(resource.FilePath))
            {
                await _fileService.DeleteFileAsync(resource.FilePath);
            }

            await _loggingService.LogActionAsync(
                resourceId,
                "Resource Deleted",
                userId,
                resource.Status.ToString(),
                "Deleted",
                $"Resource '{resource.Title}' permanently deleted"
            );

            await _unitOfWork.LearningResources.DeleteAsync(resource);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private ResourceDTO MapToDTO(LearningResource resource)
        {
            return new ResourceDTO
            {
                Id = resource.Id,
                Title = resource.Title,
                Description = resource.Description,
                ResourceType = resource.ResourceType,
                Status = resource.Status,
                FilePath = resource.FilePath,
                UploadedByName = resource.UploadedBy != null
                    ? $"{resource.UploadedBy.FirstName} {resource.UploadedBy.LastName}"
                    : "Unknown",
                UploadedDate = resource.UploadedDate,
                ApprovedByName = resource.ApprovedBy != null
                    ? $"{resource.ApprovedBy.FirstName} {resource.ApprovedBy.LastName}"
                    : null,
                ApprovedDate = resource.ApprovedDate,
                RejectionReason = resource.RejectionReason
            };
        }
    }
}
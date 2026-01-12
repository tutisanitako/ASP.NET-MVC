using Microsoft.AspNetCore.Http;

namespace AcademicResourceManagement.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folder);
        Task<bool> DeleteFileAsync(string filePath);
        bool FileExists(string filePath);
    }
}
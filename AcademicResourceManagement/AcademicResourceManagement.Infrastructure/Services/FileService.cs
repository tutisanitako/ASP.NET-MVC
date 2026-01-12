using AcademicResourceManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AcademicResourceManagement.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _webRootPath;

        public FileService()
        {
            // Get the web root path - this will be injected from the Web project
            _webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            // Create unique filename
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

            // Create folder path
            var folderPath = Path.Combine(_webRootPath, "uploads", folder);

            // Ensure directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Full file path
            var filePath = Path.Combine(folderPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for storage
            return Path.Combine("uploads", folder, fileName).Replace("\\", "/");
        }

        public Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_webRootPath, filePath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public bool FileExists(string filePath)
        {
            var fullPath = Path.Combine(_webRootPath, filePath);
            return File.Exists(fullPath);
        }
    }
}
using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicResourceManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register Infrastructure services with Dependency Injection
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
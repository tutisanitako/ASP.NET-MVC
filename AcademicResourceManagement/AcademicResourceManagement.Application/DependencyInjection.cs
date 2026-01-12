using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicResourceManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Service Layer services with Dependency Injection
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<ILoggingService, LoggingService>();

            return services;
        }
    }
}
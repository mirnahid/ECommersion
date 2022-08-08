using ECommersionAPI.Application.Services;
using ECommersionAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommersionAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}

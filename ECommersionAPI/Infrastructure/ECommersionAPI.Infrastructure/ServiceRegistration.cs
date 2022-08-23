using ECommersionAPI.Application.Abstractions.Storage;
using ECommersionAPI.Infrastructure.Enums;
using ECommersionAPI.Infrastructure.Services.Storage;
using ECommersionAPI.Infrastructure.Services.Storage.Azure;
using ECommersionAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommersionAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            //services.AddScoped<IFileService, FileService>();
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}

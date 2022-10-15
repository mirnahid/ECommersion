using ECommersionAPI.Application.Abstractions.Services;
using ECommersionAPI.Application.Abstractions.Services.Authentications;
using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Domain.Entities.Identity;
using ECommersionAPI.Persistence.Contexts;
using ECommersionAPI.Persistence.Repositories;
using ECommersionAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommersionAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceService(this IServiceCollection services)
        {
            services.AddDbContext<ECommersionAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ECommersionAPIDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriterepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInteralAuthentication, AuthService>();
        }
    }
}

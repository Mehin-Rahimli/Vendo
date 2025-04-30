

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Application.Abstractions.Services;
using Vendo.Domain.Entities;
using Vendo.Persistence.Contexts;
using Vendo.Persistence.Implementations.Repostories;
using Vendo.Persistence.Implementations.Services;

namespace Vendo.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Default")));
           
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            //Repos
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
           

            //Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<AppDbContextInitializer>();
            return services;
        }
    }
}

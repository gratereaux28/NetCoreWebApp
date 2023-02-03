using Core.Interfaces;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NetCoreWebAppContext>(x => { x.UseSqlServer(configuration.GetConnectionString("NetCoreWebAppContext"), builder => builder.CommandTimeout((int)TimeSpan.FromMinutes(120).TotalSeconds)); }, ServiceLifetime.Scoped);
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IProductCategoriesService, ProductCategoriesService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

    }
}
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
    // In this class we create some extention to potencialize the startup class
    public static class ServiceCollectionExtension
    {
        //This method contains all different context that will be inyected to our proyect
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NetCoreWebAppContext>(x => { x.UseSqlServer(configuration.GetConnectionString("NetCoreWebAppContext"), builder => builder.CommandTimeout((int)TimeSpan.FromMinutes(120).TotalSeconds)); }, ServiceLifetime.Scoped);
            return services;
        }

        //This method contains all services thar will be inyected in our proyect
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IOrderDetailsService, OrderDetailsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IProductCategoriesService, ProductCategoriesService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

    }
}
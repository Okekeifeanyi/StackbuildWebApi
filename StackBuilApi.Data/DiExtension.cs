using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackBuilApi.Core.Interface.irepositories;
using StackBuilApi.Core.Interface.iservices;
using StackBuilApi.Data.Repositories;
using StackBuilApi.Data.UOW;
using StackBuilApi.Services;
using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Data.Database;
using StackBuildApi.Data.Repositories;
using StackBuildApi.Services;

namespace StackBuildApi.Data
{
    public static class DiExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Configure DbContext to use PostgreSQL
            services.AddDbContext<StackBuildDB>(options =>
                options.UseNpgsql(config.GetConnectionString("stackbuild")));

            // Registerations
         
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
          
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



        }
    }
}

using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shops.Api.Base;
using Shops.Api.Base.DataAccess;
using Shops.Api.Middlewares;
using Shops.Api.Services;

namespace Shops.Api.Configuration
{
    public static class DependencyStartup
    {
        public static void Configure(IServiceCollection services)
        {
            ConfigureDatabase(services);
            AddServices(services);
            AddInfrastructure(services);
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(AppConfiguration.DbConnectionString));
        }
        
        private static void AddInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
        
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShopService, ShopService>();
        }
        
        public static IApplicationBuilder AddMiddlewares(this IApplicationBuilder app)
        {
            var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (currentEnvironment == "Development") return app;

            app.UseMiddleware<AccessTokenMiddleware>(AppConfiguration.ApiAccessToken);

            return app;
        }
    }
}
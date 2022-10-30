using System;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shops.App.Base.Handlers;
using Shops.App.Context;
using Shops.App.Handlers.Order;
using Shops.App.Handlers.Product;
using Shops.App.Handlers.Shop;
using Shops.App.Models.Identity;

namespace Shops.App.Configuration
{
    public static class DependencyStartup
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            AddIdentity(services);
            AddHandlers(services);
            AddInfrastructure(services);
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();
        }
        
        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        
        private static void AddInfrastructure(IServiceCollection services)
        {
            services.AddHttpClient("Api", x =>
            {
                x.BaseAddress = new Uri(AppConfiguration.ApiUrl);
                x.DefaultRequestHeaders.Add("AccessToken", AppConfiguration.ApiAccessToken);
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            
            });;
        }
        
        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped(typeof(ICreateEntityHandler<>), typeof(CreateEntityHandler<>));
            services.AddScoped(typeof(IUpdateEntityHandler<>), typeof(UpdateEntityHandler<>));
            services.AddScoped<IDeleteEntityHandler, DeleteEntityHandler>();
            services.AddScoped<IConfirmEntityHandler, ConfirmEntityHandler>();
            services.AddScoped<IDeclineEntityHandler, DeclineEntityHandler>();
            
            AddShopHandlers(services);
            AddProductHandlers(services);
            AddOrderHandlers(services);
        }

        private static void AddShopHandlers(IServiceCollection services)
        {
            services.AddScoped<IGetShopUpdateModelHandler, GetShopUpdateModelHandler>();
            services.AddScoped<IFilterShopHandler, FilterShopHandler>();
        }

        private static void AddProductHandlers(IServiceCollection services)
        {
            services.AddScoped<IFilterProductHandler, FilterProductHandler>();
            services.AddScoped<IGetProductCreateModelHandler, GetProductCreateModelHandler>();
            services.AddScoped<IGetProductUpdateModelHandler, GetProductUpdateModelHandler>();
        }

        private static void AddOrderHandlers(IServiceCollection services)
        {
            services.AddScoped<IGetOrderCreateModelHandler, GetOrderCreateModelHandler>();
            services.AddScoped<IFilterOrderHandler, FilterOrderHandler>();
            services.AddScoped<IGetOrderShowModelHandler, GetOrderShowModelHandler>();
        }
    }
}
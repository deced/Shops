using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Shops.App.Base.Handlers;
using Shops.App.Handlers.Order;
using Shops.App.Handlers.Product;
using Shops.App.Handlers.Shop;

namespace Shops.App.Configuration
{
    public static class DependencyStartup
    {
        public static void Configure(IServiceCollection services)
        {
            AddHandlers(services);
            AddInfrastructure(services);
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

            services.AddScoped<IGetShopUpdateModelHandler, GetShopUpdateModelHandler>();
            services.AddScoped<IFilterProductHandler, FilterProductHandler>();
            services.AddScoped<IFilterShopHandler, FilterShopHandler>();
            services.AddScoped<IGetProductCreateModelHandler, GetProductCreateModelHandler>();
            services.AddScoped<IGetProductUpdateModelHandler, GetProductUpdateModelHandler>();
            services.AddScoped<IGetOrderCreateModelHandler, GetOrderCreateModelHandler>();
            services.AddScoped<IFilterOrderHandler, FilterOrderHandler>();
            services.AddScoped<IConfirmEntityHandler, ConfirmEntityHandler>();
            services.AddScoped<IDeclineEntityHandler, DeclineEntityHandler>();
        }
    }
}
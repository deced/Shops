using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shops.App.Handlers.Shop
{
    public interface IFilterShopHandler
    {
        Task<FilterShopHandlerResponse> ExecuteAsync(string name);
    }
    public class FilterShopHandler : IFilterShopHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FilterShopHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FilterShopHandlerResponse> ExecuteAsync(string name)
        {
            var response = new FilterShopHandlerResponse();
            response.Items = new List<FilterShopHandlerResponseItem>();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<FilterShopHandlerResponse>($"Shop/Filter?name={name}");
            }
            catch (Exception ex)
            {
                response.Result = FilterShopHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class FilterShopHandlerResponse
    {
        public List<FilterShopHandlerResponseItem> Items { get; set; }
        public FilterShopHandlerResult Result { get; set; }
    }

    public class FilterShopHandlerResponseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int OrdersCount { get; set; }
        public int ProductsCount { get; set; }
    }

    public enum FilterShopHandlerResult
    {
        Success,
        Error
    }
}
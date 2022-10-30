using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shops.App.Handlers.Order
{
    public interface IFilterOrderHandler
    {
        Task<FilterOrderHandlerResponse> ExecuteAsync(string shopName);
    }
    public class FilterOrderHandler : IFilterOrderHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FilterOrderHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FilterOrderHandlerResponse> ExecuteAsync(string shopName)
        {
            var response = new FilterOrderHandlerResponse();
            response.Items = new List<FilterOrderHandlerResponseItem>();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<FilterOrderHandlerResponse>($"Order/Filter?shopName={shopName}");
            }
            catch (Exception ex)
            {
                response.Result = FilterOrderHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class FilterOrderHandlerResponse
    {
        public List<FilterOrderHandlerResponseItem> Items { get; set; }
        public FilterOrderHandlerResult Result { get; set; }
    }

    public class FilterOrderHandlerResponseItem
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductsCount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
    }

    public enum FilterOrderHandlerResult
    {
        Success,
        Error
    }
}
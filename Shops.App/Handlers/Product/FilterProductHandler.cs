using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Shops.App.Handlers.Product
{
    public interface IFilterProductHandler
    {
        Task<FilterProductHandlerResponse> ExecuteAsync(string name);
    }
    public class FilterProductHandler : IFilterProductHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FilterProductHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FilterProductHandlerResponse> ExecuteAsync(string name)
        {
            var response = new FilterProductHandlerResponse();
            response.Items = new List<FilterProductHandlerResponseItem>();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<FilterProductHandlerResponse>($"Product/Filter?name={name}");
            }
            catch (Exception ex)
            {
                response.Result = FilterProductHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class FilterProductHandlerResponse
    {
        public List<FilterProductHandlerResponseItem> Items { get; set; }
        public FilterProductHandlerResult Result { get; set; }
    }

    public class FilterProductHandlerResponseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
    }

    public enum FilterProductHandlerResult
    {
        Success,
        Error
    }
}
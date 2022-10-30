using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shops.App.Models.Product;

namespace Shops.App.Handlers.Product
{
    public interface IGetProductCreateModelHandler
    {
        Task<GetProductCreateModelHandlerResponse> ExecuteAsync();
    }
    public class GetProductCreateModelHandler : IGetProductCreateModelHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetProductCreateModelHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetProductCreateModelHandlerResponse> ExecuteAsync()
        {
            var response = new GetProductCreateModelHandlerResponse();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<GetProductCreateModelHandlerResponse>($"Product/GetCreateModel");
            }
            catch (Exception ex)
            {
                response.Result = GetProductCreateModelHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class GetProductCreateModelHandlerResponse
    {
        public IEnumerable<ShopDropdownModel> Shops { get; set; }
        public GetProductCreateModelHandlerResult Result { get; set; }
    }

    public enum GetProductCreateModelHandlerResult
    {
        Success,
        Error
    }
}
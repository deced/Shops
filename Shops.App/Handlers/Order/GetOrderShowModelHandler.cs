using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shops.App.Models.Order;

namespace Shops.App.Handlers.Order
{
    public interface IGetOrderShowModelHandler
    {
        Task<GetOrderShowModelHandlerResponse> ExecuteAsync(int id);
    }
    public class GetOrderShowModelHandler : IGetOrderShowModelHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetOrderShowModelHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetOrderShowModelHandlerResponse> ExecuteAsync(int id)
        {
            var response = new GetOrderShowModelHandlerResponse();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<GetOrderShowModelHandlerResponse>($"Order/GetShowModel/{id}");
            }
            catch (Exception ex)
            {
                response.Result = GetOrderShowModelHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class GetOrderShowModelHandlerResponse
    {
        public int OrderId { get; set; }
        public string ShopName { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<ShowOrderedProductModel> Products { get; set; }
        public GetOrderShowModelHandlerResult Result { get; set; }
    }

    public enum GetOrderShowModelHandlerResult
    {
        Success,
        NoSuchOrder,
        Error
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shops.App.Models.Order;
using Shops.App.Models.Product;

namespace Shops.App.Handlers.Order
{
    public interface IGetOrderCreateModelHandler
    {
        Task<GetOrderCreateModelHandlerResponse> ExecuteAsync(int shopId);
    }
    public class GetOrderCreateModelHandler : IGetOrderCreateModelHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetOrderCreateModelHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetOrderCreateModelHandlerResponse> ExecuteAsync(int shopId)
        {
            var response = new GetOrderCreateModelHandlerResponse();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<GetOrderCreateModelHandlerResponse>($"Order/GetCreateModel/{shopId}");
            }
            catch (Exception ex)
            {
                response.Result = GetOrderCreateModelHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class GetOrderCreateModelHandlerResponse
    {
        public IEnumerable<CreateOrderProductModel> Products { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public GetOrderCreateModelHandlerResult Result { get; set; }
    }

    public enum GetOrderCreateModelHandlerResult
    {
        Success,
        NoSuchOrder,
        Error
    }
}
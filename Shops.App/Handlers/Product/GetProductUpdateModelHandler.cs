using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shops.App.Handlers.Product
{
    public interface IGetProductUpdateModelHandler
    {
        Task<GetProductUpdateModelHandlerResponse> ExecuteAsync(int id);
    }
    public class GetProductUpdateModelHandler : IGetProductUpdateModelHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetProductUpdateModelHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetProductUpdateModelHandlerResponse> ExecuteAsync(int id)
        {
            var response = new GetProductUpdateModelHandlerResponse();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<GetProductUpdateModelHandlerResponse>($"Product/GetUpdateModel/{id}");
            }
            catch (Exception ex)
            {
                response.Result = GetProductUpdateModelHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class GetProductUpdateModelHandlerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
        public GetProductUpdateModelHandlerResult Result { get; set; }
    }

    public enum GetProductUpdateModelHandlerResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
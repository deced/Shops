using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shops.App.Handlers.Shop
{
    public interface IGetShopUpdateModelHandler
    {
        Task<GetShopUpdateModelHandlerResponse> ExecuteAsync(int id);
    }
    public class GetShopUpdateModelHandler : IGetShopUpdateModelHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetShopUpdateModelHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetShopUpdateModelHandlerResponse> ExecuteAsync(int id)
        {
            var response = new GetShopUpdateModelHandlerResponse();
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                response = await client.GetFromJsonAsync<GetShopUpdateModelHandlerResponse>($"Shop/GetUpdateModel/{id}");
            }
            catch (Exception ex)
            {
                response.Result = GetShopUpdateModelHandlerResult.Error;
                return response;
            }
            return response;
        }
    }

    public class GetShopUpdateModelHandlerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public GetShopUpdateModelHandlerResult Result { get; set; }
    }

    public enum GetShopUpdateModelHandlerResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
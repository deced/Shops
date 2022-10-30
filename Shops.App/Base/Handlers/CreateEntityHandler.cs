using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shops.App.Base.Models;

namespace Shops.App.Base.Handlers
{
    public interface ICreateEntityHandler<TModel> where TModel : CreateHandlerModel
    {
        Task<CreateEntityHandlerResponse> ExecuteAsync(string controllerName, TModel model);
    }

    public class CreateEntityHandler<TModel> : ICreateEntityHandler<TModel> where TModel : CreateHandlerModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateEntityHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CreateEntityHandlerResponse> ExecuteAsync(string controllerName, TModel model)
        {
            var response = new CreateEntityHandlerResponse();

            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                var request = await client.PostAsJsonAsync($"{controllerName}/Create", model);
                response = await request.Content.ReadFromJsonAsync<CreateEntityHandlerResponse>();
            }
            catch (Exception ex)
            {
                response.Result = CreateEntityHandlerResult.Error;
                return response;
            }

            return response;
        }
    }

    public class CreateEntityHandlerResponse
    {
        public CreateEntityHandlerResult Result { get; set; }
        public int EntityId { get; set; }
    }

    public enum CreateEntityHandlerResult
    {
        Success,
        AlreadyExists,
        Error
    }
}
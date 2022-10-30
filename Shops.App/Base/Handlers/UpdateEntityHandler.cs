using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shops.App.Base.Models;

namespace Shops.App.Base.Handlers
{
    public interface IUpdateEntityHandler<TModel> where TModel : UpdateHandlerModel
    {
        Task<UpdateEntityHandlerResponse> ExecuteAsync(string controllerName,TModel model);
    }
    public class UpdateEntityHandler<TModel> : IUpdateEntityHandler<TModel> where TModel : UpdateHandlerModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateEntityHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UpdateEntityHandlerResponse> ExecuteAsync(string controllerName,TModel model)
        {
            var response = new UpdateEntityHandlerResponse();

            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                var request = await client.PutAsJsonAsync($"{controllerName}/Update", model);
                response = await request.Content.ReadFromJsonAsync<UpdateEntityHandlerResponse>();
            }
            catch (Exception ex)
            {
                response.Result = UpdateEntityHandlerResult.Error;
                return response;
            }

            return response;
        }
    }
    
    public class UpdateEntityHandlerResponse
    {
        public UpdateEntityHandlerResult Result { get; set; }
    }

    public enum UpdateEntityHandlerResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Shops.App.Base.Handlers
{
    public interface IDeleteEntityHandler
    {
        Task<DeleteEntityHandlerResponse> ExecuteAsync(string controllerName,int id);
    }
    public class DeleteEntityHandler : IDeleteEntityHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteEntityHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        
        public async Task<DeleteEntityHandlerResponse> ExecuteAsync(string controllerName,int id)
        {
            var response = new DeleteEntityHandlerResponse();

            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                var request = await client.DeleteAsync($"{controllerName}/Delete?id={id}");
                response = await request.Content.ReadFromJsonAsync<DeleteEntityHandlerResponse>();
            }
            catch (Exception ex)
            {
                response.Result = DeleteEntityHandlerResult.Error;
                return response;
            }

            return response;
        }
    }
    
    public class DeleteEntityHandlerResponse
    {
        public DeleteEntityHandlerResult Result { get; set; }
    }
    public enum DeleteEntityHandlerResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shops.App.Base.Handlers
{
    public interface IConfirmEntityHandler
    {
        Task<ConfirmEntityHandlerResponse> ExecuteAsync(string controllerName,int id);
    }
    public class ConfirmEntityHandler : IConfirmEntityHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ConfirmEntityHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        
        public async Task<ConfirmEntityHandlerResponse> ExecuteAsync(string controllerName,int id)
        {
            var response = new ConfirmEntityHandlerResponse();

            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                var request = await client.PutAsync($"{controllerName}/Confirm?id={id}",null);
                response = await request.Content.ReadFromJsonAsync<ConfirmEntityHandlerResponse>();
            }
            catch (Exception ex)
            {
                response.Result = ConfirmEntityHandlerResult.Error;
                return response;
            }

            return response;
        }
    }
    
    public class ConfirmEntityHandlerResponse
    {
        public ConfirmEntityHandlerResult Result { get; set; }
    }
    public enum ConfirmEntityHandlerResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
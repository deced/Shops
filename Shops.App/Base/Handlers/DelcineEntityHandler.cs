using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shops.App.Base.Handlers
{
    public interface IDeclineEntityHandler
    {
        Task<DeclineEntityHandlerResponse> ExecuteAsync(string controllerName,int id);
    }
    public class DeclineEntityHandler : IDeclineEntityHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeclineEntityHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        
        public async Task<DeclineEntityHandlerResponse> ExecuteAsync(string controllerName,int id)
        {
            var response = new DeclineEntityHandlerResponse();

            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                var request = await client.PutAsync($"{controllerName}/Decline?id={id}",null);
                response = await request.Content.ReadFromJsonAsync<DeclineEntityHandlerResponse>();
            }
            catch (Exception ex)
            {
                response.Result = DeclineEntityHandlerResult.Error;
                return response;
            }

            return response;
        }
    }
    
    public class DeclineEntityHandlerResponse
    {
        public DeclineEntityHandlerResult Result { get; set; }
    }
    public enum DeclineEntityHandlerResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
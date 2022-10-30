using Shops.Api.Models.Shop.Responses;

namespace Shops.Api.Base.Response
{
    public class CreateEntityResponse
    {
        public CreateEntityResponseResult Result { get; set; }
        public int EntityId { get; set; }
    }

    public enum CreateEntityResponseResult
    {
        Success,
        AlreadyExists,
        Error
    }
}
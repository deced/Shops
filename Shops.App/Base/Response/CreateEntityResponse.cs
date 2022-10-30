namespace Shops.App.Base.Response
{
    public class CreateEntityResponse
    {
        public CreateEntityResponseResult Result { get; set; }
        public int EntityId { get; set; }
    }

    public enum CreateEntityResponseResult
    {
        Success,
        Error
    }
}
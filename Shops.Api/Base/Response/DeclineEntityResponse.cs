namespace Shops.Api.Base.Response
{
    public class DeclineEntityResponse
    {
        public DeclineEntityResponseResult Result { get; set; }
    }

    public enum DeclineEntityResponseResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
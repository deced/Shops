namespace Shops.Api.Base.Response
{
    public class DeleteEntityResponse
    {
        public  DeleteEntityResponseResult Result { get; set; }
    }

    public enum DeleteEntityResponseResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
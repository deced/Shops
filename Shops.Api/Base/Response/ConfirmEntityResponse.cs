namespace Shops.Api.Base.Response
{
    public class ConfirmEntityResponse
    {
        public ConfirmEntityResponseResult Result { get; set; }
    }

    public enum ConfirmEntityResponseResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
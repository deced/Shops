namespace Shops.App.Base.Response
{
    public class UpdateEntityResponse
    {
        public UpdateEntityRepositoryResult Result { get; set; }
    }

    public enum UpdateEntityRepositoryResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
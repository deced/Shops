namespace Shops.Api.Models.Shop.Responses
{
    public class ShopUpdateOpenModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ShopUpdateOpenModelResponseResult Result { get; set; }
    }

    public enum ShopUpdateOpenModelResponseResult
    {
        Success,
        NoSuchEntity,
        Error
    }
}
namespace Shops.Api.Models.Products.Responses
{
    public class ProductUpdateOpenModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
        public ProductUpdateOpenModelResponseResult Result { get; set; }
    }

    public enum ProductUpdateOpenModelResponseResult
    {
        Success,
        Error
    }
}
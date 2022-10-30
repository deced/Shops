namespace Shops.Api.Models.Products
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int ShopId { get; set; }
    }
}
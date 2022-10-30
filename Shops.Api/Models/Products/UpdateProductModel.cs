namespace Shops.Api.Models.Products
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
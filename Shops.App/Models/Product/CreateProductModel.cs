using Shops.App.Base.Models;

namespace Shops.App.Models.Product
{
    public class CreateProductModel : CreateHandlerModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int ShopId { get; set; }
    }
}
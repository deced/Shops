using Shops.App.Base.Models;

namespace Shops.App.Models.Product
{
    public class UpdateProductModel : UpdateHandlerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
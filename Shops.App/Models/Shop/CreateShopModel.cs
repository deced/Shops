using Shops.App.Base.Models;

namespace Shops.App.Models.Shop
{
    public class CreateShopModel : CreateHandlerModel
    {
        public string Name { get; set; }
        public string Address { get;set; }
    }
}
using Shops.App.Base.Models;

namespace Shops.App.Models.Shop
{
    public class UpdateShopModel : UpdateHandlerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get;set; }
    }
}
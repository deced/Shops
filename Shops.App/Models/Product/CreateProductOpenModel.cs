using System.Collections.Generic;
using Shops.App.Handlers.Product;

namespace Shops.App.Models.Product
{
    public class CreateProductOpenModel
    {
        public IEnumerable<ShopDropdownModel> Shops { get; set; }
    }
    
    public class ShopDropdownModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
using System.Collections;
using System.Collections.Generic;

namespace Shops.App.Models.Product
{
    public class ProductIndexModel
    {
        public IEnumerable<ProductIndexModelItem> Items { get; set; }
    }

    public class ProductIndexModelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int OrdersCount { get; set; }
    }
}
using System.Collections.Generic;

namespace Shops.App.Models.Order
{
    public class ShowOrderModel
    {
        public int OrderId { get; set; }
        public string ShopName { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<ShowOrderedProductModel> Products { get; set; }
    }

    public class ShowOrderedProductModel

    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
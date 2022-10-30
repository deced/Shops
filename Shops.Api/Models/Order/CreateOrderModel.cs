using System.Collections.Generic;

namespace Shops.Api.Models.Order
{
    public class CreateOrderModel
    {
        public int ShopId { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<int> ProductIds { get; set; }
    }
}
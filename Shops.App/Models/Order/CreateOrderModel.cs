using System.Collections.Generic;
using Shops.App.Base.Models;

namespace Shops.App.Models.Order
{
    public class CreateOrderModel : CreateHandlerModel
    {
        public IEnumerable<int> ProductIds { get; set; }
        public string ShippingAddress { get; set; }
        public int ShopId { get; set; }
    }
}
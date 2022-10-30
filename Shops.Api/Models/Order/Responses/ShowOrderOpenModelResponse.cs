using System.Collections.Generic;
using System.Dynamic;
using Shops.Api.Entities;

namespace Shops.Api.Models.Order.Responses
{
    public class ShowOrderOpenModelResponse
    {
        public int OrderId { get; set; }
        public string ShopName { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<OrderedProductModel> Products { get; set; }
        public ShowOrderOpenModelResponseResult Result { get; set; }
    }

    public class OrderedProductModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }

    public enum ShowOrderOpenModelResponseResult
    {
        Success,
        NoSuchOrder,
        Error
    }
    
}
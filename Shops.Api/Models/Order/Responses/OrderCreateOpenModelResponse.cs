using System.Collections.Generic;

namespace Shops.Api.Models.Order.Responses
{
    public class OrderCreateOpenModelResponse
    {
        public IEnumerable<OrderCreateProductModel> Products { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public OrderCreateOpenModelResponseResult Result { get; set; }
    }

    public class OrderCreateProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }

    public enum OrderCreateOpenModelResponseResult
    {
        Success,
        NoSuchShop,
        Error
    }
}
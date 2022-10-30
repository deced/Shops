using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shops.Api.Entities;

namespace Shops.Api.Models.Order.Responses
{
    public class FilterOrderResponse
    {
        public IEnumerable<FilterOrderResponseItem> Items { get; set; }
        public FilterOrderResponseResult Result { get; set; }
    }

    public class FilterOrderResponseItem
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductsCount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }
    }

    public enum FilterOrderResponseResult
    {
        Success,
        Error
    }
}
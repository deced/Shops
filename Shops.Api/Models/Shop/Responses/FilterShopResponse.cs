using System.Collections.Generic;

namespace Shops.Api.Models.Shop.Responses
{
    public class FilterShopResponse
    {
        public IEnumerable<FilterShopResponseItem> Items { get; set; }
        public FilterShopResponseResult Result { get; set; }
    }

    public class FilterShopResponseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int OrdersCount { get; set; }
        public int ProductsCount { get; set; }
    }

    public enum FilterShopResponseResult
    {
        Success,
        Error
    }
}
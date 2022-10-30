using System.Collections.Generic;

namespace Shops.Api.Models.Products.Responses
{
    public class FilterProductResponse
    {
        public IEnumerable<FilterProductResponseItem> Items { get; set; }
        public FilterProductResponseResult Result { get; set; }
    }

    public class FilterProductResponseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
    }

    public enum FilterProductResponseResult
    {
        Success,
        Error
    }
}
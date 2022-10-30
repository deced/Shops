using System.Collections.Generic;

namespace Shops.Api.Models.Products.Responses
{
    public class ProductCreateOpenModelResponse
    {
        public IEnumerable<ShopDropdownModel> Shops { get; set; }
        public ProductCreateOpenModelResponseResult Result { get; set; } 
    }
    
    public class ShopDropdownModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public enum ProductCreateOpenModelResponseResult
    {
        Success,
        Error
    }
    
}
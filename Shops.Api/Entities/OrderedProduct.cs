using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shops.Api.Base;

namespace Shops.Api.Entities
{
    public class OrderedProduct : Document
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public decimal Price { get; set; }
        
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
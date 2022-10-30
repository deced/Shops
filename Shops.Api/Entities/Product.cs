using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shops.Api.Base;

namespace Shops.Api.Entities
{
    public class Product : Document
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }
        [Required]
        public int ShopId { get; set; }
    }
}
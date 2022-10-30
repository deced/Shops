using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shops.Api.Base;

namespace Shops.Api.Entities
{
    public class Shop : Document
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get;set; }
        public virtual ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shops.Api.Base;

namespace Shops.Api.Entities
{
    public class Order : Document
    {
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
        [Required]
        public int ShopId { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderedProduct> Products { get; set; }
    }
    
    

    public enum OrderStatus
    {
        Waiting,
        Completed,
        Declined
    }
    
}
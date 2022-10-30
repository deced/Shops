using System.Collections.Generic;

namespace Shops.App.Models.Order
{
    public class CreateOrderOpenModel
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public IEnumerable<CreateOrderProductModel> Products { get; set; }
    }

    public class CreateOrderProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopApi.Model
{
    [Table("OrderItems")]
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; } = new Order();
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}
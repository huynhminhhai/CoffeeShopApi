using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopApi.Model
{
    public enum OrderStatus
    {
        Pending,
        Served,
        Paid
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string TableNumber { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set;} = new List<OrderItem>();
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopApi.Model
{
    [Table("Customers")]
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Point { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }        
}
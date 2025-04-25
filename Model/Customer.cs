using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopApi.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Point { get; set; }
    }        
}
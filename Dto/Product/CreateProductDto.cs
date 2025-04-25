using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopApi.Dto.Product
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
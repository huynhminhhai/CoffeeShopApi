using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.Product;

namespace CoffeeShopApi.Dto.Category
{
    public class CategoryDtoWithProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
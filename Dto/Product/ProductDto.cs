using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.ProductImage;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public List<ProductImageDto> ProductImages { get; set; } = new List<ProductImageDto>();
    }
}
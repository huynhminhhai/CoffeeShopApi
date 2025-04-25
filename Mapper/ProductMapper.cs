using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.Product;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Mapper
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductRequestDto requestDto)
        {
            return new Product
            {
                Name = requestDto.Name,
                Price = requestDto.Price,
                CategoryId = requestDto.CategoryId
            };
        }
    }
}
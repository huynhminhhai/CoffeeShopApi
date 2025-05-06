using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.Product;
using CoffeeShopApi.Dto.ProductImage;
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
                CategoryId = product.CategoryId,
                ProductImages = product.ProductImages.Select(pi => new ProductImageDto
                {
                    ImageId = pi.Image.Id,
                    Url = pi.Image.Url
                }).ToList()
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductRequestDto requestDto)
        {
            return new Product
            {
                Name = requestDto.Name,
                Price = requestDto.Price,
                CategoryId = requestDto.CategoryId,
                ProductImages = requestDto.ProductImages.Select(id => new ProductImage
                {
                    ImageId = id,
                }).ToList()
            };
        }
    }
}
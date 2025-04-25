using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.Category;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static CategoryDtoWithProduct ToCategoryDtoWithProduct(this Category category)
        {
            return new CategoryDtoWithProduct
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(p => p.ToProductDto()).ToList()
            };
        }

        public static Category ToCategory(this CreateCategoryRequestDto requestCategory)
        {
            return new Category
            {
                Name = requestCategory.Name
            };
        }
    }
}
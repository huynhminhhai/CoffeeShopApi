using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.ProductImage;

namespace CoffeeShopApi.Dto.Product
{
    public class UpdateProductRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Name must be at most 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1,000,000")]
        public decimal Price { get; set; }
        public List<CreateProductImageDto> ProductImages { get; set; } = new List<CreateProductImageDto>();
    }
}
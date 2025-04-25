using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Product;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICaterogyRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICaterogyRepository caterogyRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = caterogyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryObject queryObject)
        {
            var products = await _productRepository.GetProductsAsync(queryObject);

            var productsDto = products.Select(p => p.ToProductDto()).ToList();

            return Ok(ApiResponse<List<ProductDto>>.SuccessResponse(productsDto, "Get products successfully"));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Product not found", 404));
            }

            return Ok(ApiResponse<ProductDto>.SuccessResponse(product.ToProductDto(), "Create product successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto requestProduct)
        {

            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            if (!await _categoryRepository.CategoryExists(requestProduct.CategoryId))
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Category not found", 404));
            }

            var product = requestProduct.ToProductFromCreateDto();

            var createdProduct = await _productRepository.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, ApiResponse<ProductDto>.SuccessResponse(product.ToProductDto(), "Create product successfully"));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto requestProduct)
        {

            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            var product = await _productRepository.UpdateProductAsync(requestProduct, id);

            if (product == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Product not found", 404));
            }

            return Ok(product.ToProductDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _productRepository.DeleteProductAsync(id);

            if (product == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Product not found", 404));
            }

            return Ok(ApiResponse<ProductDto>.SuccessResponse(product.ToProductDto(), "Delete product successfully"));
        }
    }
}
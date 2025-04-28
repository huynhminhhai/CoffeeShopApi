using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Category;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controller
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICaterogyRepository _categoryRepository;

        public CategoryController(ICaterogyRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryQueryObject query)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(query);

            var categoriesDto = categories.Select(c => c.ToCategoryDto()).ToList();

            return Ok(ApiResponse<List<CategoryDto>>.SuccessResponse(categoriesDto, "Get all categories successfully"));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Category not found", 404));
            }

            return Ok(ApiResponse<CategoryDtoWithProduct>.SuccessResponse(category.ToCategoryDtoWithProduct(), "Get category successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto requestCategory)
        {

            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            var categoryModel = requestCategory.ToCategory();

            var category = await _categoryRepository.CreateCategoryAsync(categoryModel);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id },
                ApiResponse<CategoryDto>.SuccessResponse(category.ToCategoryDto(), "Create category successfully"));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequestDto updateCategory)
        {

            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            var category = await _categoryRepository.UpdateCategoryAsync(id, updateCategory);

            if (category == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Category not found", 404));
            }

            return Ok(ApiResponse<CategoryDto>.SuccessResponse(category.ToCategoryDto(), "Update category successfully"));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.DeleteCategoryAsync(id);

            if (category == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Category not found", 404));
            }

            return Ok(ApiResponse<CategoryDto>.SuccessResponse(category.ToCategoryDto(), "Delete category successfully"));
        }
    }
}
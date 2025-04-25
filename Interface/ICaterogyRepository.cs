using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.Category;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Interface
{
    public interface ICaterogyRepository
    {
        Task<List<Category>> GetAllCategoriesAsync(CategoryQueryObject query);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryRequestDto updateCategory);
        Task<Category?> DeleteCategoryAsync(int id);
        Task<bool> CategoryExists(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Data;
using CoffeeShopApi.Dto.Category;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Repository
{
    public class CategoryRepository : ICaterogyRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CategoryQueryObject queryObject)
        {

            var categories = _context.Categories.Include(p => p.Products).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Name))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(queryObject.Name.ToLower()));
            }

            var skipNumber = (queryObject.PageIndex - 1) * queryObject.PageSize;

            return await categories.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            return category;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryRequestDto updateCategory)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            category.Name = updateCategory.Name;

            _context.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
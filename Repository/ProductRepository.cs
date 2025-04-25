using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Data;
using CoffeeShopApi.Dto.Product;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync(ProductQueryObject queryObject)
        {

            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Name))
            {
                products = products.Where(p => p.Name.ToLower().Contains(queryObject.Name.ToLower()));
            }

            if (queryObject.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >= queryObject.MinPrice.Value);
            }

            if (queryObject.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= queryObject.MaxPrice.Value);
            }

            if (queryObject.CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == queryObject.CategoryId.Value);
            }

            var skipNumber = (queryObject.PageIndex - 1) * queryObject.PageSize;

            return await products.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateProductAsync(UpdateProductRequestDto requestProduct, int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            product.Name = requestProduct.Name;
            product.Price = requestProduct.Price;

            _context.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
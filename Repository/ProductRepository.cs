using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Data;
using CoffeeShopApi.Dto.Product;
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

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
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
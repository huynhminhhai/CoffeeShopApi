using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Dto.Product;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(ProductQueryObject queryObject);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(UpdateProductRequestDto requestProduct, int id);
        Task<Product?> DeleteProductAsync(int id);
    }
}
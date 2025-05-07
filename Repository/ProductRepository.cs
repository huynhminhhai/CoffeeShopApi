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

            var products = _context.Products.Include(p => p.ProductImages)
            .ThenInclude(pi => pi.Image).AsQueryable();

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
            var product = await _context.Products
                .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                .FirstOrDefaultAsync(p => p.Id == id);

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

            var productWithImage = await _context.Products
                .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            return productWithImage;
        }

        public async Task<Product?> UpdateProductAsync(UpdateProductRequestDto requestProduct, int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            // Cập nhật thông tin cơ bản
            product.Name = requestProduct.Name;
            product.Price = requestProduct.Price;

            // Lấy danh sách ID ảnh cũ và mới
            var currentImageIds = product.ProductImages.Select(pi => pi.ImageId).ToList();
            var newImageIds = requestProduct.ProductImages;

            // Tìm ảnh cần xóa
            var imageIdsToRemove = currentImageIds.Except(newImageIds).ToList();
            foreach (var imageId in imageIdsToRemove)
            {
                var productImage = product.ProductImages.FirstOrDefault(pi => pi.ImageId == imageId);
                if (productImage != null)
                {
                    _context.ProductImages.Remove(productImage);

                    // Xóa Image nếu không còn product nào dùng
                    bool isImageUsedElsewhere = await _context.ProductImages
                        .AnyAsync(pi => pi.ImageId == imageId && pi.ProductId != product.Id);

                    if (!isImageUsedElsewhere)
                    {
                        var image = await _context.Images.FindAsync(imageId);
                        if (image != null)
                        {
                            _context.Images.Remove(image);
                        }
                    }
                }
            }

            // Tìm ảnh cần thêm
            var imageIdsToAdd = newImageIds.Except(currentImageIds).ToList();
            foreach (var imageId in imageIdsToAdd)
            {
                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = product.Id,
                    ImageId = imageId
                });
            }

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

            foreach (var productImage in product.ProductImages)
            {
                var image = productImage.Image;

                // Xoá ProductImage
                _context.ProductImages.Remove(productImage);

                // Kiểm tra nếu image không còn được liên kết với sản phẩm nào khác thì xoá luôn
                bool isImageUsedElsewhere = await _context.ProductImages
                    .AnyAsync(pi => pi.ImageId == image.Id && pi.ProductId != product.Id);

                if (!isImageUsedElsewhere)
                {
                    _context.Images.Remove(image);
                }
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
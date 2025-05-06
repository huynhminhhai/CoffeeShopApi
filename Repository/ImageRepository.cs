using CoffeeShopApi.Data;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Repository
{
    public class ImageRepository: IImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImageRepository(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<Image> UploadImageAsync(Image image)
        {
            _context.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
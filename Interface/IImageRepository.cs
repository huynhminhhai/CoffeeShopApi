using CoffeeShopApi.Model;

namespace CoffeeShopApi.Interface
{
    public interface IImageRepository
    {
        Task<Image> UploadImageAsync(Image image);
    }
}
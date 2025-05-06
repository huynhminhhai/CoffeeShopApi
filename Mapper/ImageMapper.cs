using CoffeeShopApi.Dto.Image;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Mapper
{
    public static class ImageMapper
    {
        public static ImageDto ToImageDto(this Image image)
        {
            return new ImageDto
            {
                Id = image.Id,
                Url = image.Url,
                Filename = image.Filename,
                CreatedAt = image.CreatedAt
            };
        }

        public static Image ToImageFromCreateDto(this CreateImageRequestDto createImageRequestDto)
        {
            return new Image
            {
                Url = createImageRequestDto.Url,
                Filename = createImageRequestDto.Filename
            };
        }
    }
}
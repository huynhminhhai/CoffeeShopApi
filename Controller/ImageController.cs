using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Image;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Mapper;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controller
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            // extension
            List<string> validExtensions = new List<string> { ".jpg", ".png", ".jpeg", ".gif", ".webp" };
            string extension = Path.GetExtension(image.FileName).ToLowerInvariant().Trim();
            if (!validExtensions.Contains(extension))
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid file extension", 400));
            }

            // file size
            int maxFileSize = 5 * 1024 * 1024; // 5MB
            if (image.Length > maxFileSize)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("File size is too large", 400));
            }

            // name changing
            string originalName = Path.GetFileNameWithoutExtension(image.FileName);
            string safeName = originalName.Replace(" ", "_");
            string fileName = $"{Guid.NewGuid()}-{safeName}{extension}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            try
            {
                var imageModel = new Image
                {
                    Filename = fileName,
                    Url = $"/uploads/{fileName}"
                };

                var imageResult = await _imageRepository.UploadImageAsync(imageModel);

                await using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
                await image.CopyToAsync(stream);

                return Ok(ApiResponse<ImageDto>.SuccessResponse(imageResult.ToImageDto(), "Upload image successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, 400));
            }

        }
    }
}
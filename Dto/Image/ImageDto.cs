namespace CoffeeShopApi.Dto.Image
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
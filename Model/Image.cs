using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopApi.Model
{
    [Table("Images")]
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
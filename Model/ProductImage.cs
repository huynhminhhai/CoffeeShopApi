using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopApi.Model
{
    [Table("ProductImages")]
    public class ProductImage
    {
        public Product Product { get; set; }
        public Image Image { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }
    }
}
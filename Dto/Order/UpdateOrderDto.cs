using System.ComponentModel.DataAnnotations;

namespace CoffeeShopApi.Dto.Order
{
    public class UpdateOrderRequestDto
    {
        [Required(ErrorMessage = "TotalAmount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
        public decimal TotalAmount { get; set; }
    }
}
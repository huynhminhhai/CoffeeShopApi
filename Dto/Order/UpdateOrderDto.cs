using System.ComponentModel.DataAnnotations;
using CoffeeShopApi.Dto.OrderItem;

namespace CoffeeShopApi.Dto.Order
{
    public class UpdateOrderRequestDto
    {
        [Required(ErrorMessage = "OrderId is required")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "CustomerId is required")]
        public int? CustomerId { get; set; }
        [Required(ErrorMessage = "TotalAmount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
        public List<UpdateOrderItemRequestDto> OrderItems { get; set; } = new();
    }
}
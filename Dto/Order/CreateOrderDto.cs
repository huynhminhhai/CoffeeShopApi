using System.ComponentModel.DataAnnotations;
using CoffeeShopApi.Dto.OrderItem;

namespace CoffeeShopApi.Dto.Order
{
    public class CreateOrderRequestDto
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int? CustomerId { get; set; }
        [MinLength(1, ErrorMessage = "Order must have at least one item.")]
        public List<CreateOrderItemRequestDto> OrderItems { get; set; } = new();
    }
}
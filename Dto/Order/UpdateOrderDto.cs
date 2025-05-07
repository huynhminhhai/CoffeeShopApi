using System.ComponentModel.DataAnnotations;
using CoffeeShopApi.Dto.OrderItem;

namespace CoffeeShopApi.Dto.Order
{
    public class UpdateOrderRequestDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "CustomerId is required")]
        public int? CustomerId { get; set; }
        [MinLength(1, ErrorMessage = "Order must have at least one item.")]
        public List<UpdateOrderItemRequestDto> OrderItems { get; set; } = new();
    }
}
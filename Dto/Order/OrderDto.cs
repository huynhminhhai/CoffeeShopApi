using CoffeeShopApi.Dto.Customer;
using CoffeeShopApi.Dto.OrderItem;

namespace CoffeeShopApi.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public CustomerDtoWithoutOrders? Customer { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
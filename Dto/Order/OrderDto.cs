using CoffeeShopApi.Model;

namespace CoffeeShopApi.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
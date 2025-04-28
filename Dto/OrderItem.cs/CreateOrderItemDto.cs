namespace CoffeeShopApi.Dto.OrderItem
{
    public class CreateOrderItemRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
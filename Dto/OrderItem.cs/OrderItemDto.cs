namespace CoffeeShopApi.Dto.OrderItem
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
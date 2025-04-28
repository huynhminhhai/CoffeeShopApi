using CoffeeShopApi.Dto.Order;
using CoffeeShopApi.Dto.OrderItem;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Customer = order.Customer != null ? order.Customer.ToCustomerDtoWithoutOrders() : null,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList() ?? new List<OrderItemDto>(),
                TotalAmount = order.TotalAmount
            };
        }

        public static Order ToOrderFromCreateDto(this CreateOrderRequestDto requestDto)
        {
            return new Order
            {
                CustomerId = requestDto.CustomerId,
                OrderItems = requestDto.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }
    }
}
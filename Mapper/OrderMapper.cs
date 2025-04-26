using CoffeeShopApi.Dto.Order;
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
                TotalAmount = order.TotalAmount
            };
        }

        public static Order ToOrderFromCreateDto(this CreateOrderRequestDto requestDto)
        {
            return new Order
            {
                TotalAmount = requestDto.TotalAmount
            };
        }
    }
}
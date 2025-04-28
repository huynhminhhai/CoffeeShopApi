using CoffeeShopApi.Dto.Order;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Interface
{
    public interface IOrderRepository
    {
        public Task<(List<Order>, int)> GetAllOrdersAsync(OrderQueryObject queryObject);
        public Task<Order?> GetOrderByIdAsync(int id);
        public Task<Order> CreateOrderAsync(Order order);
        public Task<Order?> UpdateOrderAsync(UpdateOrderRequestDto updateOrderDto, int orderId);
        public Task<Order?> DeleteOrderAsync(int id);
    }
}
using CoffeeShopApi.Helper;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Interface
{
    public interface IOrderItemRepository
    {
        Task<(List<OrderItem>, int)> GetAllOrderItemsAsync(OrderItemQueryObject orderItemQueryObject);
    }
}
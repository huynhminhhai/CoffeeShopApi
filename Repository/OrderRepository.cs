using CoffeeShopApi.Data;
using CoffeeShopApi.Dto.Order;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAllOrdersAsync(OrderQueryObject queryObject)
        {
            var orders = _context.Orders.AsQueryable();

            if (queryObject.StartDate.HasValue)
            {
                orders = orders.Where(o => o.CreatedAt >= queryObject.StartDate.Value);
            }

            if (queryObject.EndDate.HasValue)
            {
                orders = orders.Where(o => o.CreatedAt <= queryObject.EndDate.Value);
            }

            var skipNumber = (queryObject.PageIndex - 1) * queryObject.PageSize;

            return orders.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderAsync(UpdateOrderRequestDto updateOrderDto)
        {
            throw new NotImplementedException();
        }
    }
}
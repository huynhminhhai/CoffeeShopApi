using CoffeeShopApi.Data;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context; 

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<OrderItem>, int)> GetAllOrderItemsAsync(OrderItemQueryObject orderItemQueryObject)
        {
            var orderItems = _context.OrderItems.AsQueryable();

            if (orderItemQueryObject.OrderId.HasValue)
            {
                orderItems = orderItems.Where(oi => oi.OrderId == orderItemQueryObject.OrderId.Value);
            }

            var totalItems = _context.OrderItems.Count();

            var skipNumber = (orderItemQueryObject.PageIndex - 1) * orderItemQueryObject.PageSize;

            var result = await orderItems
                .Skip(skipNumber)
                .Take(orderItemQueryObject.PageSize)
                .ToListAsync();

            return (result, totalItems);
        }
    }
}
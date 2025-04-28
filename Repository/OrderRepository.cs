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

        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order.OrderItems == null || order.OrderItems.Count == 0)
            {
                throw new ArgumentException("Order must have at least one OrderItem.");
            }

            decimal totalAmount = 0;

            foreach (var orderItem in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(orderItem.ProductId);

                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {orderItem.ProductId} not found.");
                }

                orderItem.UnitPrice = product.Price;
                totalAmount += orderItem.Quantity * orderItem.UnitPrice;
            }

            order.TotalAmount = totalAmount;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            if (order.CustomerId.HasValue)
            {
                order.Customer = await _context.Customers.FindAsync(order.CustomerId.Value);
            }

            return order;
        }

        public Task<Order> DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<Order>, int)> GetAllOrdersAsync(OrderQueryObject queryObject)
        {
            var orders = _context.Orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.PhoneNumber))
            {
                orders = orders.Where(o => o.Customer.PhoneNumber.ToLower().Contains(queryObject.PhoneNumber.ToLower()));
            }

            if (queryObject.StartDate.HasValue)
            {
                orders = orders.Where(o => o.CreatedAt >= queryObject.StartDate.Value);
            }

            if (queryObject.EndDate.HasValue)
            {
                orders = orders.Where(o => o.CreatedAt <= queryObject.EndDate.Value);
            }

            var totalItems = await _context.Orders.CountAsync();

            var skipNumber = (queryObject.PageIndex - 1) * queryObject.PageSize;

            var result = await orders
                .Skip(skipNumber)
                .Take(queryObject.PageSize)
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .ToListAsync();

            return (result, totalItems);
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
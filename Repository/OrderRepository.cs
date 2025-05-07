using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<Order?> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return null;
            }

            foreach (var orderItem in order.OrderItems)
            {
                _context.OrderItems.Remove(orderItem);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<(List<Order>, int)> GetAllOrdersAsync(OrderQueryObject queryObject)
        {
            var orders = _context.Orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.PhoneNumber))
            {
                orders = orders.Where(o => o.Customer != null &&
                                          o.Customer.PhoneNumber.ToLower().Contains(queryObject.PhoneNumber.ToLower()));
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

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return null;
            }

            return order;
        }

        public async Task<Order?> UpdateOrderAsync(UpdateOrderRequestDto updateOrderDto)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == updateOrderDto.Id);

            if (order == null)
            {
                return null;
            }

            // Update CustomerId
            order.CustomerId = updateOrderDto.CustomerId;

            // Get List Products
            var productIds = updateOrderDto.OrderItems.Select(oi => oi.ProductId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            // Update OrderItems
            var updatedProductIds = updateOrderDto.OrderItems.Select(oi => oi.ProductId).ToList();

            // Delete OrderItem Not In Request Dto Anymore
            order.OrderItems.RemoveAll(oi => !updatedProductIds.Contains(oi.ProductId));

            // Update Or Add OrderItem
            foreach (var updateItem in updateOrderDto.OrderItems)
            {
                var existingItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == updateItem.ProductId);

                var product = products.FirstOrDefault(p => p.Id == updateItem.ProductId);

                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {updateItem.ProductId} not found.");
                }

                if (existingItem != null)
                {
                    existingItem.Quantity = updateItem.Quantity;
                    existingItem.UnitPrice = product.Price;
                }
                else
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = updateItem.ProductId,
                        Quantity = updateItem.Quantity,
                        UnitPrice = product.Price
                    });
                }
            }

            // Update TotalAmount
            order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);

            _context.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
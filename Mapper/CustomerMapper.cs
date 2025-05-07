using CoffeeShopApi.Dto.Customer;
using CoffeeShopApi.Dto.Order;
using CoffeeShopApi.Dto.OrderItem;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToCustomerDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Point = customer.Point,
                Orders = customer.Orders?.Select(o => new OrderDto
                {
                    Id = o.Id,
                    CreatedAt = o.CreatedAt,
                    OrderItems = o.OrderItems?.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList() ?? new List<OrderItemDto>()
                }).ToList() ?? new List<OrderDto>()
            };
        }

        public static CustomerDtoWithoutOrders ToCustomerDtoWithoutOrders(this Customer customer)
        {
            return new CustomerDtoWithoutOrders
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Point = customer.Point
            };
        }

        public static Customer ToCustomerFromCreateCustomerDto(this CreateCustomerRequestDto createCustomerDto)
        {
            return new Customer
            {
                FullName = createCustomerDto.FullName,
                PhoneNumber = createCustomerDto.PhoneNumber,
                Point = 0
            };
        }
    }
}
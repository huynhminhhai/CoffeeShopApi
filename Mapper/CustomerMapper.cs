using CoffeeShopApi.Dto.Customer;
using CoffeeShopApi.Dto.Order;
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
                    TotalAmount = o.TotalAmount
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
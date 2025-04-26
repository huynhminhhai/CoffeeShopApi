using CoffeeShopApi.Dto.Customer;
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
                Orders = customer.Orders.Select(o => o.ToOrderDto()).ToList()
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
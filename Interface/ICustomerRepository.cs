using CoffeeShopApi.Dto.Customer;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Model;

namespace CoffeeShopApi.Interface
{
    public interface ICustomerRepository
    {
        public Task<(List<Customer>, int)> GetCustomersAsync(CustomerQueryObject queryObject);
        public Task<Customer?> GetCustomerByIdAsync(int id);
        public Task<Customer> CreateCustomerAsync(Customer customer);
        public Task<Customer?> UpdateCustomerAsync(UpdateCustomerRequestDto updateCustomerDto);
        public Task<Customer?> DeleteCustomerAsync(int id);
        public Task<bool> PhoneNumberExists(string phoneNumber);
    }
}
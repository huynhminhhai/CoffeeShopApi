using CoffeeShopApi.Data;
using CoffeeShopApi.Dto.Customer;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return null;
            }

            _context.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public async Task<(List<Customer>, int)> GetCustomersAsync(CustomerQueryObject queryObject)
        {
            var customers = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.FullName))
            {
                customers = customers.Where(c => c.FullName.ToLower().Contains(queryObject.FullName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.PhoneNumber))
            {
                customers = customers.Where(c => c.PhoneNumber.ToLower().Contains(queryObject.PhoneNumber.ToLower()));
            }

            var totalItems = await _context.Customers.CountAsync();

            var skipNumber = (queryObject.PageIndex - 1) * queryObject.PageSize;
            var result = await customers
                .OrderBy(c => c.Id)
                .Skip(skipNumber)
                .Take(queryObject.PageSize)
                .ToListAsync();

            return (result, totalItems);
        }

        public async Task<bool> PhoneNumberExists(string phoneNumber)
        {
            return await _context.Customers.AnyAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<Customer?> UpdateCustomerAsync(UpdateCustomerRequestDto updateCustomerDto, int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return null;
            }

            customer.FullName = updateCustomerDto.FullName;
            customer.Point = updateCustomerDto.Point;

            _context.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
using CoffeeShopApi.Dto.Order;

namespace CoffeeShopApi.Dto.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Point { get; set; }
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
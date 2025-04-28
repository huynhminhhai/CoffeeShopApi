using CoffeeShopApi.Dto.Order;

namespace CoffeeShopApi.Dto.Customer
{
    public class CustomerDtoWithoutOrders
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Point { get; set; }
    }
}
namespace CoffeeShopApi.Helper
{
    public class CustomerQueryObject
    {
        public string? PhoneNumber { get; set; } = null;
        public string? FullName { get; set; } = null;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
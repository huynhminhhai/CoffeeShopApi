namespace CoffeeShopApi.Helper
{
    public class OrderItemQueryObject
    {
        public int? OrderId { get; set; } = null;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
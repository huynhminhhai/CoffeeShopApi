using CoffeeShopApi.Dto.OrderItem;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Mapper;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controller
{
    [Route("api/order-item")]
    [ApiController]
    public class OrderItemController: ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems([FromQuery] OrderItemQueryObject queryObject)
        {
            var (orderItems, totalItems) = await _orderItemRepository.GetAllOrderItemsAsync(queryObject);

            var orderItemsDto = orderItems.Select(o => o.ToOrderItemDto()).ToList();

            return Ok(PageApiResponse<List<OrderItemDto>>.SuccessPageResponse(
                data: orderItemsDto,
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize,
                totalItems: totalItems,
                message: "Get order items successfully"
            )); 
        }
    }
}
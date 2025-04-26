using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Order;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controller
{
    [Route("api/order")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderQueryObject queryObject)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(queryObject);

            var ordersDto = orders.Select(o => o.ToOrderDto()).ToList();

            return Ok(ApiResponse<List<OrderDto>>.SuccessResponse(ordersDto, "Get all orders successfully"));
        }
    }
}
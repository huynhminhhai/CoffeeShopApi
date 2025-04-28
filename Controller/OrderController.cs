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
            var (orders, totalItems) = await _orderRepository.GetAllOrdersAsync(queryObject);

            var ordersDto = orders.Select(o => o.ToOrderDto()).ToList();

            // return Ok(ApiResponse<List<OrderDto>>.SuccessResponse(ordersDto, "Get all orders successfully"));

            return Ok(PageApiResponse<List<OrderDto>>.SuccessPageResponse(
                data: ordersDto,
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize,
                totalItems: totalItems,
                message: "Get orders successfully"
            ));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            var order = requestDto.ToOrderFromCreateDto();

            try
            {
                var createdOrder = await _orderRepository.CreateOrderAsync(order);

                return Ok(ApiResponse<OrderDto>.SuccessResponse(createdOrder.ToOrderDto(), "Create order successfully"));
            } catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, 400));
            }
        }
    }
}
using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Customer;
using CoffeeShopApi.Helper;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controller
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerQueryObject queryObject)
        {
            var (customers, totalItems) = await _customerRepository.GetCustomersAsync(queryObject);

            var customersDto = customers.Select(c => c.ToCustomerDto()).ToList();

            return Ok(PageApiResponse<List<CustomerDto>>.SuccessPageResponse(
                data: customersDto,
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize,
                totalItems: totalItems,
                message: "Get customers successfully"
            ));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Customer not found", 404));
            }

            return Ok(ApiResponse<CustomerDto>.SuccessResponse(customer.ToCustomerDto(), "Get customer successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequestDto createCustomerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            if (await _customerRepository.PhoneNumberExists(createCustomerRequestDto.PhoneNumber))
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Phone number already exists", 400));
            }

            var customer = createCustomerRequestDto.ToCustomerFromCreateCustomerDto();

            customer = await _customerRepository.CreateCustomerAsync(customer);

            return Ok(ApiResponse<CustomerDto>.SuccessResponse(customer.ToCustomerDto(), "Create customer successfully"));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] UpdateCustomerRequestDto updateCustomerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            var customer = await _customerRepository.UpdateCustomerAsync(updateCustomerRequestDto, id);

            if (customer == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Customer not found", 404));
            }

            return Ok(ApiResponse<CustomerDto>.SuccessResponse(customer.ToCustomerDto(), "Update customer successfully"));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var customer = await _customerRepository.DeleteCustomerAsync(id);

            if (customer == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Customer not found", 404));
            }

            return Ok(ApiResponse<CustomerDto>.SuccessResponse(customer.ToCustomerDto(), "Delete customer successfully"));
        }
    }
}
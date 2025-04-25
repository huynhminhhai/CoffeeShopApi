
using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Account;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var firstError = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .FirstOrDefault()?.ErrorMessage;

                    return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
                }

                var appUser = new AppUser()
                {
                    UserName = registerRequestDto.UserName,
                    Email = registerRequestDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerRequestDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(ApiResponse<AppUser>.SuccessResponse(appUser, "Register successfully"));
                    }
                    else
                    {
                        var firstRoleError = roleResult.Errors.FirstOrDefault()?.Description;

                        return BadRequest(ApiResponse<string>.ErrorResponse(firstRoleError ?? "Validation failed", 400));
                    }
                }
                else
                {
                    var firstRoleError = createdUser.Errors.FirstOrDefault()?.Description;

                    return BadRequest(ApiResponse<string>.ErrorResponse(firstRoleError ?? "Validation failed", 400));
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse("An error occurred: " + e.Message, 500));
            }
        }
    }
}
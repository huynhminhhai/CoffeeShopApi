
using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Account;
using CoffeeShopApi.Interface;
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
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
                        var newUserDto = new NewUserDto()
                        {
                            Username = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        };

                        return Ok(ApiResponse<NewUserDto>.SuccessResponse(newUserDto, "Register successfully"));
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
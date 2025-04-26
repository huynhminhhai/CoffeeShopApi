
using CoffeeShopApi.Common;
using CoffeeShopApi.Dto.Account;
using CoffeeShopApi.Interface;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
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


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return BadRequest(ApiResponse<string>.ErrorResponse(firstError ?? "Validation failed", 400));
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);

            if (user == null)
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("User not found", 401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("Usernam not found and/or password incorrect", 401));
            }

            var res = new NewUserDto()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };

            return Ok(ApiResponse<NewUserDto>.SuccessResponse(res, "Login successfully"));
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.DTOs;
using SecureVault.Application.Interfaces;
using System.Security.Claims;
using System.Security.Claims;


namespace SecureVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto dto)
        {
            

            var result = await _userService.CreateUserAsync(dto);
            return Ok(result);

        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUserAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Something went wrong."
                });
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {


            try
            {
               
                var result = await _userService.LoginAsync(dto);

                if (result == null)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = "Invalid email or password"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    data = result
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Something went wrong. Please try again later."
                });
            }
        }


        [Authorize]
        [HttpGet("me")]
        public IActionResult GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userId == null)
                return Unauthorized();

            return Ok(new
            {
                success = true,
                user = new
                {
                    id = userId,
                    email = email
                }
            });
        }





    }
}

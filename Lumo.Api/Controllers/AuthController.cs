using Lumo.Api.DTOs;
using Lumo.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(CreateUserRequest userRequest)
        {
            var user = await _userService.RegisterUserAsync(userRequest);
            if(user == null)
            {
                return Problem("Email already exists", statusCode: StatusCodes.Status409Conflict);
            }
            return Created((string?)null, user);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = await _userService.LoginUserAsync(loginRequest);

            if(response == null)
            {
                return Problem("Invalid email or password", statusCode: StatusCodes.Status401Unauthorized);
            }
            return Ok(response);
        }
    }
}

using bsc_be.Services;
using Microsoft.AspNetCore.Mvc;
using bsc_be.DTOs;

namespace bsc_be.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthControllers : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthControllers(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResponse? response = await _authService.LoginAsync(request);
            if(response == null)
            {
                return Unauthorized(new {status = "Error", message = "Invalid username or password"});
            }
            return Ok(response);
        }
    }
}
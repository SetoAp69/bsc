using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserProfile(long id)
        {
            var user = await _userService.GetUserProfileAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
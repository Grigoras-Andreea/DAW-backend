using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("verify")]
        public async Task<ActionResult> VerifyUser([FromBody] UserModel loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Name) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var user = await _userService.VerifyUserAsync(loginRequest.Name, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] UserModel user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("User details are invalid.");
            }

            try
            {
                await _userService.CreateUserAsync(user);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

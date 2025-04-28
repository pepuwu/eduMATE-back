using eduMATE_back.Models;
using eduMATE_back.Services;
using Microsoft.AspNetCore.Mvc;

namespace eduMATE_back.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = await _authService.RegisterAsync(request);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _authService.LoginAsync(request);
            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(user);
        }
    }
}

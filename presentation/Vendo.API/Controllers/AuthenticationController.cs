using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendo.Application.Abstractions.Services;
using Vendo.Application.DTOs.AppUsers;

namespace Vendo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto userDto)
        {
            await _service.RegisterAsync(userDto);
            return NoContent();
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromForm] LoginDto userDto)
        {
            return Ok(await _service.LoginAsync(userDto));
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> LoginByRefresh(string refToken)
        {
            return Ok(await _service.LoginByRefreshToken(refToken));
        }

    }
}

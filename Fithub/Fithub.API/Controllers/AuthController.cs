using Fithub.API.Interfaces;
using Fithub.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fithub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Credentials credentials)
        {
            var authInput = new AuthData() { Authentication = credentials };
            var authOutput = await _authService.AuthenticateAsync(authInput);

            if (authOutput.Authentication == null)
                return Unauthorized();

            return Ok(authOutput.Authentication);
        }
    }
}

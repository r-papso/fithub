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
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Authenticate([FromBody] Credentials credentials)
        {
            var authInput = new AuthData() { Authentication = credentials };
            var authOutput = await _authService.Authenticate(authInput);

            if (authOutput.Authentication == null)
                return Unauthorized();

            return Ok(authOutput.Authentication as User);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] Credentials credentials)
        {
            var user = new User() { Username = credentials.Username, Password = credentials.Password };
            var result = await _userService.AddUser(user);

            if (result == null)
                return BadRequest();

            var authInput = new AuthData() { Authentication = credentials };
            var authOutput = await _authService.Authenticate(authInput);

            if (authOutput.Authentication == null)
                return Unauthorized();

            return Ok(authOutput.Authentication);
        }
    }
}

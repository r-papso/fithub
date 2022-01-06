using Fithub.API.Helpers;
using Fithub.API.Interfaces;
using Fithub.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fithub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<User>> Get()
        {
            var user = await _userService.GetUserByIdAsync(GetUserId());

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        // PUT api/<UserController>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] User user)
        {
            user.Id = GetUserId();
            var result = await _userService.UpdateUserAsync(user);
            return GetActionResult(result);
        }

        // DELETE api/<UserController>
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete()
        {
            var user = new User() { Id = GetUserId() };
            var result = await _userService.DeleteUserAsync(user);
            return GetActionResult(result);
        }
    }
}

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
    public class UserController : ControllerBase
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

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            var result = await _userService.AddUserAsync(user);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] User user)
        {
            user.Id = GetUserId();
            var result = await _userService.UpdateUserAsync(user);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete()
        {
            var user = new User() { Id = GetUserId() };
            var result = await _userService.DeleteUserAsync(user);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        private int GetUserId()
        {
            return int.Parse(HttpContext.Items["User"].ToString());
        }
    }
}

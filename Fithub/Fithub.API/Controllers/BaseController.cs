using Microsoft.AspNetCore.Mvc;

namespace Fithub.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(HttpContext.Items["User"].ToString());
        }

        protected ActionResult GetActionResult(bool operationResult)
        {
            if (operationResult)
                return Ok();
            else
                return BadRequest();
        }
    }
}

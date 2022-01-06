using Microsoft.AspNetCore.Mvc;

namespace Fithub.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(HttpContext.Items["User"].ToString());
        }

        protected ActionResult GetActionResult(object operationResult)
        {
            return operationResult != null ? Ok(operationResult) : BadRequest();
        }
    }
}

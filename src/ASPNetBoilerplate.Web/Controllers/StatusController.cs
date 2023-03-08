using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetBoilerplate.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/")]
    public class StatusController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Status()
        {
            return Ok("Hi! The API is up and running!");
        }
    }
}

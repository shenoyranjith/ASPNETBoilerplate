using ASPNetBoilerplate.Web.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetBoilerplate.Web.Controllers
{
    /// <summary>
    /// Represents the base controller class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Policy = "Authenticated")]
    [CommonExceptionFilter]
    public class BaseController : ControllerBase
    {
    }
}

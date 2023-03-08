using ASPNetBoilerplate.Web.CustomFilters;
using ASPNetBoilerplate.Web.Models.ClientModels.Authentication;
using ASPNetBoilerplate.Web.Models.ResponseModels;
using ASPNetBoilerplate.Web.Models.ResponseModels.Authentication;
using ASPNetBoilerplate.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ASPNetBoilerplate.Web.Controllers
{
    /// <summary>
    /// Handles Requests related to Authentication
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    [CommonExceptionFilter]
    [AuthenticationExceptionFilter]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// The custom authentication service
        /// </summary>
        private readonly ICustomAuthenticationService _customAuthService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="customAuthService">The custom authentication service.</param>
        public AuthenticationController(ICustomAuthenticationService customAuthService)
        {
            _customAuthService = customAuthService;
        }

        /// <summary>
        /// POST v1.0/Authentication/Login
        /// Validates Login Requests and responds with JWT Tokens based on level of authentication
        /// </summary>
        /// <param name="loginModel">Login Parameters</param>
        /// <returns>
        /// Http Response
        /// </returns>
        /// <exception cref="System.ArgumentNullException">loginModel</exception>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login loginModel)
        {
            if (loginModel is null)
            {
                throw new ArgumentNullException(nameof(loginModel));
            }

            var authenticatedUser = _customAuthService.Authenticate(loginModel);

            var response = new AuthenticationResponse
            {
                AccessToken = _customAuthService.BuildAuthenticatedToken(authenticatedUser),
                UserId = authenticatedUser.Id.ToString(CultureInfo.InvariantCulture),
                UserRole = authenticatedUser.UserRole
            };
            return Accepted(new ResponseModel() { Data = response });
        }
    }
}

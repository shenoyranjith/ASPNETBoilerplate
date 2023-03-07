using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Web.Models.ClientModels.Authentication;

namespace ASPNetBoilerplate.Web.Services.Interfaces
{
    /// <summary>
    /// Operations related to Custom Authentication
    /// </summary>
    public interface ICustomAuthenticationService
    {
        /// <summary>
        /// Authenticates the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>
        /// Validated User
        /// </returns>
        User Authenticate(Login login);

        /// <summary>
        /// Builds the authenticated token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Authenticated JWT Token
        /// </returns>
        string BuildAuthenticatedToken(User user);
    }
}

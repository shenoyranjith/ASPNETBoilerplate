using ASPNetBoilerplate.Domain.Enumerations;

namespace ASPNetBoilerplate.Web.Models.ResponseModels.Authentication
{
    /// <summary>
    /// Authentication Response Model
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public UserRole UserRole { get; set; }
    }
}

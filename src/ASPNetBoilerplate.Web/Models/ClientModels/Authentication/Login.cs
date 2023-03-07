using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ASPNetBoilerplate.Web.Models.ClientModels.Authentication
{
    /// <summary>
    /// The Login Model
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}

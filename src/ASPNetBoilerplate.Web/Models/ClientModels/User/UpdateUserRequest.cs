using ASPNetBoilerplate.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace ASPNetBoilerplate.Web.Models.ClientModels.User
{
    /// <summary>
    /// The user update model
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [MaxLength(256)]
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [MaxLength(256)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [MaxLength(256)]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [MaxLength(256)]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [MaxLength(256)]
        public string? Password { get; set; }
    }
}

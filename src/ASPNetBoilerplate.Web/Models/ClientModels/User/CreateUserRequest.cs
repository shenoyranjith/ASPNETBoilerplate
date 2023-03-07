using ASPNetBoilerplate.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace ASPNetBoilerplate.Web.Models.ClientModels.User
{
    /// <summary>
    /// The user create model
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

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
        [Required]
        [MaxLength(256)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}

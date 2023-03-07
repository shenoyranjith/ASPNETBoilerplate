using ASPNetBoilerplate.Domain.Enumerations;

namespace ASPNetBoilerplate.Web.Models.Filters
{
    /// <summary>
    /// Filter for users
    /// </summary>
    public class UserFilter
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public UserRole? UserRole { get; set; }
    }
}

using ASPNetBoilerplate.Domain.Enumerations;

namespace ASPNetBoilerplate.Domain.Entities
{
    /// <summary>
    /// Represents the User
    /// </summary>
    /// <seealso cref="ASPNetBoilerplate.Domain.Entities.BaseEntity" />
    public class User : BaseEntity
    {
        /// <summary>
        /// Gets or sets User's Username
        /// </summary>
        /// <value>
        /// User's Username
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets User's FirstName
        /// </summary>
        /// <value>
        /// User's FirstName
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets User's LastName
        /// </summary>
        /// <value>
        /// User's LastName
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets User's EmailAddress
        /// </summary>
        /// <value>
        /// User's EmailAddress
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets User's Password
        /// </summary>
        /// <value>
        /// User's Password
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets User's LoginAttempts
        /// </summary>
        /// <value>
        /// User's LoginAttempts
        /// </value>
        public int? LoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public UserRole UserRole { get; set; }
    }
}

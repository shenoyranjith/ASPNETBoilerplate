using ASPNetBoilerplate.Domain.Enumerations;

namespace ASPNetBoilerplate.Web.Models.ResponseModels.User
{
    /// <summary>
    /// The user response model
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

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

        /// <summary>
        /// Gets or sets User's CreatedAt
        /// </summary>
        /// <value>
        /// User's CreatedAt
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets User's UpdatedAt
        /// </summary>
        /// <value>
        /// User's UpdatedAt
        /// </value>
        public DateTime UpdatedAt { get; set; }
    }
}

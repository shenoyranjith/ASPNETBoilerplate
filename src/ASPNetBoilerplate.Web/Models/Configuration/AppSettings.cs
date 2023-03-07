using Microsoft.AspNetCore.Authentication.OAuth;

namespace ASPNetBoilerplate.Web.Models.Configuration
{
    /// <summary>
    /// Represents app config settings model
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets DB connection string
        /// </summary>
        /// <value>
        /// DB connection string
        /// </value>
        public ConnectionStrings ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the authentication options.
        /// </summary>
        /// <value>
        /// The authentication options.
        /// </value>
        public AuthOptions AuthOptions { get; set; }
    }
}

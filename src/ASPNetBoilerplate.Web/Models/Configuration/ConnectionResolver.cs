using ASPNetBoilerplate.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace ASPNetBoilerplate.Web.Models.Configuration
{
    /// <inheritdoc/>
    public class ConnectionResolver : IConnectionResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionResolver" /> class.
        /// </summary>
        /// <param name="config">The appsettings configuration</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        public ConnectionResolver(IOptions<AppSettings> config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            ConnectionString = config.Value.ConnectionStrings.DefaultConnection;
        }

        /// <summary>
        /// Gets or sets DB connection string
        /// </summary>
        /// <value>
        /// DB connection string
        /// </value>
        /// <inheritdoc cref="IConnectionResolver.ConnectionString" />
        public string ConnectionString { get; set; }
    }
}

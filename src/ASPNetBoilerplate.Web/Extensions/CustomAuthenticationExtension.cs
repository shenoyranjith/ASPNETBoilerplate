using ASPNetBoilerplate.Web.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ASPNetBoilerplate.Web.Extensions
{
    /// <summary>
    /// Custom JWT Based Authentication
    /// </summary>
    public static class CustomAuthenticationExtension
    {
        /// <summary>
        /// Adds the custom authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, null);
        }
    }
}

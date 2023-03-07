using ASPNetBoilerplate.Web.Services.Interfaces;
using ASPNetBoilerplate.Web.Services;

namespace ASPNetBoilerplate.Web.Extensions
{
    /// <summary>
    /// Add Custom Services here
    /// </summary>
    public static class CustomServicesExtension
    {
        /// <summary>
        /// Configures the custom services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}

using ASPNetBoilerplate.Web.Services.Interfaces;
using ASPNetBoilerplate.Web.Services;

namespace ASPNetBoilerplate.Web.Extensions
{
    /// <summary>
    /// Add Custom Util Services here
    /// </summary>
    public static class CustomUtilServicesExtension
    {
        /// <summary>
        /// Configures the util custom services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureCustomUtilServices(this IServiceCollection services)
        {
            services.AddSingleton<ICryptographyService, CryptographyService>();
        }
    }
}

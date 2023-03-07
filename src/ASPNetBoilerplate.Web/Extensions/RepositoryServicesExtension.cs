using ASPNetBoilerplate.Repository.Repositories;
using ASPNetBoilerplate.Repository.Repositories.Interfaces;

namespace ASPNetBoilerplate.Web.Extensions
{
    /// <summary>
    /// Core Repository Service Extension
    /// </summary>
    public static class RepositoryServicesExtension
    {
        /// <summary>
        /// Configures the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

using System.Security.Claims;

namespace ASPNetBoilerplate.Web.Extensions
{
    /// <summary>
    /// Custom Policy based Authorization
    /// </summary>
    public static class CustomAuthorizationExtension
    {
        /// <summary>
        /// Adds the custom authorization.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Authenticated",
                    policy => policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == ClaimTypes.Authentication && c.Value.Trim().ToUpperInvariant() == "TRUE")));

                options.AddPolicy(
                     "Admin",
                     policy => policy.RequireAssertion(
                         context => context.User.HasClaim(
                             c => c.Type == ClaimTypes.UserData && c.Value == "Admin")));
            });
        }
    }
}

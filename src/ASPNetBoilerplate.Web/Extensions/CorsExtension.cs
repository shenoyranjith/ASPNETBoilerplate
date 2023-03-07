namespace ASPNetBoilerplate.Web.Extensions
{
    /// <summary>
    /// Extension methods to add CORS middlewares
    /// </summary>
    public static class CorsExtension
    {
        /// <summary>
        /// Uses the custom cors.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        public static void UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(policy =>
            {
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.AllowCredentials();
            });
        }
    }
}

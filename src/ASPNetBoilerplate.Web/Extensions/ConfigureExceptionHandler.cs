using ASPNetBoilerplate.Web.CustomExceptions;
using ASPNetBoilerplate.Web.Enummerations;
using ASPNetBoilerplate.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace ASPNetBoilerplate.Web.Extensions
{
    /// <summary>
    /// Exception Middleware to catch Global Exceptions
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// Handles Global exception with errorCode 1
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder" /></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (
                            contextFeature.Error is InvalidCredentialsException ||
                            contextFeature.Error is SecurityTokenSignatureKeyNotFoundException
                        )
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await context.Response.WriteAsync(new CustomError(ErrorCode.InvalidLoginCredentials, contextFeature.Error.Message).ToString()).ConfigureAwait(false);
                        }
                        else if (contextFeature.Error is EntityDoesNotExistException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(new CustomError(ErrorCode.EntityDoesNotExist, contextFeature.Error.Message).ToString()).ConfigureAwait(false);
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            await context.Response.WriteAsync(new CustomError(ErrorCode.InternalServerError, "Something went wrong. please try again. If problem persists, please contact support.").ToString()).ConfigureAwait(false);
                        }
                    }
                });
            });
        }
    }
}

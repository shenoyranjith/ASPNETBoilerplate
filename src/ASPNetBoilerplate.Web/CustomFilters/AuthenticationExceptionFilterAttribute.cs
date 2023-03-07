using ASPNetBoilerplate.Web.CustomExceptions;
using ASPNetBoilerplate.Web.Enummerations;
using ASPNetBoilerplate.Web.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNetBoilerplate.Web.CustomFilters
{
    /// <summary>
    /// Exception Filter Attribute for Authentication Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute" />
    public sealed class AuthenticationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Overridden Implementation of OnException to handle Exception events occuring in Authentication controller
        /// </summary>
        /// <param name="context"><inheritdoc cref="ExceptionContext"/></param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            switch (context.Exception)
            {
                case InvalidCredentialsException exception:
                    context.Result = new ErrorResult(ErrorCode.InvalidLoginCredentials, exception);
                    break;
                case LoginAttemptsExceededException exception:
                    context.Result = new ErrorResult(ErrorCode.LoginAttemptsExceeded, exception);
                    break;
                case DataBaseUpdateFailureException exception:
                    context.Result = new ErrorResult(ErrorCode.DataBaseUpdateFailureException, exception);
                    break;
                case MalformedJWTokenReceivedException exception:
                    context.Result = new ErrorResult(ErrorCode.MalformedJWTokenReceivedException, exception);
                    break;
                case AggregateException ag:
                    context.Result = new ErrorResult(ErrorCode.InternalServerError, ag.Flatten());
                    break;
            }

            base.OnException(context);
        }
    }
}

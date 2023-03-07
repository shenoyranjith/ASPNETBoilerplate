using ASPNetBoilerplate.Web.CustomExceptions;
using ASPNetBoilerplate.Web.Enummerations;
using ASPNetBoilerplate.Web.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace ASPNetBoilerplate.Web.CustomFilters
{
    /// <summary>
    /// Exception Filter Class used to handle common custom exceptions
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute" />
    public sealed class CommonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Overridden implementation of OnException method
        /// </summary>
        /// <param name="context">The exception context</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            SetExceptionContextResult(context);
            base.OnException(context);
        }

        /// <summary>
        /// Sets the exception context result.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SetExceptionContextResult(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case InvalidRequestDataException ex:
                    context.Result = new ErrorResult(ErrorCode.InvalidRequestData, ex);
                    break;
                case EntityAlreadyExistsException ex:
                    context.Result = new ErrorResult(ErrorCode.EntityAlreadyExists, ex);
                    break;
                case EntityDoesNotExistException ex:
                    context.Result = new ErrorResult(ErrorCode.EntityDoesNotExist, ex);
                    break;
                case EntitiesNotFoundException ex:
                    context.Result = new ErrorResult(ErrorCode.EntitiesNotFoundException, ex);
                    break;
                case HttpRequestException ex:
                    context.Result = new ErrorResult(ErrorCode.HttpException, ex);
                    break;
                case SecurityTokenExpiredException ex:
                    context.Result = new ErrorResult(ErrorCode.TokenExpiredException, ex);
                    break;
                case UnsupportedOperationException ex:
                    context.Result = new ErrorResult(ErrorCode.UnsupportedOperation, ex);
                    break;
                case AggregateException ag:
                    context.Result = new ErrorResult(ErrorCode.InternalServerError, ag.Flatten());
                    break;
                case MalformedJWTokenReceivedException ex:
                    context.Result = new ErrorResult(ErrorCode.MalformedJWTokenReceivedException, ex);
                    break;
                case UserMismatchException ex:
                    context.Result = new ErrorResult(ErrorCode.UserMismatchException, ex);
                    break;
            }
        }
    }
}

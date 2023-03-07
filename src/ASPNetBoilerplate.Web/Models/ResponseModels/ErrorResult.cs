using ASPNetBoilerplate.Web.Enummerations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ASPNetBoilerplate.Web.Models.ResponseModels
{
    /// <summary>
    /// Custom ErrorResult Class, Inherits JsonResult
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.JsonResult" />
    public class ErrorResult : JsonResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult" /> class based on errorcode.
        /// </summary>
        /// <param name="code"><inheritdoc cref="ErrorCode" /></param>
        /// <param name="exception">The exception encountered in the Authentication Controller <see cref="Exception" /></param>
        /// <exception cref="System.ArgumentNullException">exception</exception>
        public ErrorResult(ErrorCode code, Exception exception)
                : base(null)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            StatusCode = (int)HttpStatusCode.InternalServerError;

            switch (code)
            {
                case ErrorCode.InvalidLoginCredentials:
                case ErrorCode.LoginAttemptsExceeded:
                case ErrorCode.MalformedJWTokenReceivedException:
                case ErrorCode.TokenExpiredException:
                    StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case ErrorCode.EntityAlreadyExists:
                case ErrorCode.InvalidRequestData:
                case ErrorCode.UnsupportedOperation:
                case ErrorCode.UserMismatchException:
                    StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ErrorCode.EntitiesNotFoundException:
                case ErrorCode.EntityDoesNotExist:
                    StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ErrorCode.InternalServerError:
                case ErrorCode.UnknownError:
                case ErrorCode.DataBaseUpdateFailureException:
                case ErrorCode.HttpException:
                    break;
                default:
                    break;
            }

            Value = string.IsNullOrEmpty(exception.Message)
                ? new CustomError(code, "If problem persists, please contact support.")
                : new CustomError(code, exception);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult" /> class with value and serializer settings.
        /// </summary>
        /// <param name="value">Value for ErrorResult</param>
        /// <param name="serializerSettings">JsonSerializerSettings</param>
        public ErrorResult(object value, JsonSerializerSettings serializerSettings)
            : base(value, serializerSettings)
        {
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

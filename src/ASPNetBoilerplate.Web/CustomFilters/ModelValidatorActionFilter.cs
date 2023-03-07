using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetBoilerplate.Web.CustomFilters
{
    /// <summary>
    /// Custom Action Filter to Validate Models in API Controllers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IActionFilter" />
    public class ModelValidatorActionFilter : IActionFilter
    {
        /// <summary>
        /// Called when action executing.
        /// Handles Model Validation and sends a HTTP 400 Bad Request Response if model state is invalid
        /// </summary>
        /// <param name="context">The ActionExecuting filter context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        /// <summary>
        /// Called when action executed.
        /// Currently Empty
        /// </summary>
        /// <param name="context">The ActionExecuting filter context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}

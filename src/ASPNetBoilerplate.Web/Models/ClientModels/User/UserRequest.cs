using ASPNetBoilerplate.Web.Models.Filters;

namespace ASPNetBoilerplate.Web.Models.ClientModels.User
{
    /// <summary>
    /// The user request model
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public UserFilter? Filter { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; } = 100;

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; } = 1;
    }
}

namespace ASPNetBoilerplate.Web.Models.ResponseModels
{
    /// <summary>
    /// The response model
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public long? TotalCount { get; set; }
    }
}

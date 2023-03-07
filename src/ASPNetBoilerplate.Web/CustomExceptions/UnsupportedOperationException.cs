namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Represents the class for the exception which is thrown when requested feature/operation not yet supported.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UnsupportedOperationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedOperationException" /> class.
        /// </summary>
        /// <param name="unsupportedFeature">The unsupported feature/operation for ex. Integration, lineItem execution etc</param>
        /// <param name="instance">The instance ex. Amazon, LineItem etc.</param>
        public UnsupportedOperationException(string unsupportedFeature, string instance)
            : base($"The {unsupportedFeature} for {instance} not yet supported")
        {
        }
    }
}

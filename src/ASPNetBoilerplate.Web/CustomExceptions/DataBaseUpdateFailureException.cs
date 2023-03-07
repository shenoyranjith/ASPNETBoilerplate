namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// DataBase Update Failure Exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class DataBaseUpdateFailureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseUpdateFailureException" /> class.
        /// </summary>
        public DataBaseUpdateFailureException()
            : base("Values Not Updated")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseUpdateFailureException" /> class.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DataBaseUpdateFailureException(Exception innerException)
            : base("Values Not Updated", innerException)
        {
        }
    }
}

namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// User Mismatch Exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserMismatchException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMismatchException" /> class.
        /// </summary>
        public UserMismatchException()
            : base("User is not authorized to perform this action.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMismatchException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UserMismatchException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMismatchException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UserMismatchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Invalid BusinessProfile requested exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class MalformedJWTokenReceivedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MalformedJWTokenReceivedException" /> class.
        /// </summary>
        public MalformedJWTokenReceivedException()
            : base($"Invalid JSON Web Token was received")
        {
        }
    }
}

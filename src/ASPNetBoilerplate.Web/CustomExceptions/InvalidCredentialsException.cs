namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Invalid LoginPassword Exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class InvalidCredentialsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException" /> class.
        /// </summary>
        public InvalidCredentialsException()
            : base("Invalid Credentials")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException" /> class.
        /// </summary>
        /// <param name="maxLoginAttempts">Max Login Attempts</param>
        /// <param name="currentLoginAttempts">Current Login Attempts</param>
        public InvalidCredentialsException(int maxLoginAttempts, int currentLoginAttempts)
            : base("Invalid Credentials")
        {
            Data.Add("maxLoginAttempts", maxLoginAttempts);
            Data.Add("currentLoginAttempts", currentLoginAttempts);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException" /> class.
        /// </summary>
        /// <param name="maxLoginAttempts">Max Login Attempts</param>
        /// <param name="currentLoginAttempts">Current Login Attempts</param>
        /// <param name="innerException">The Inner <see cref="Exception" /> which occurred</param>
        public InvalidCredentialsException(int maxLoginAttempts, int currentLoginAttempts, Exception innerException)
            : base("Invalid Credentials", innerException)
        {
            Data.Add("maxLoginAttempts", maxLoginAttempts);
            Data.Add("currentLoginAttempts", currentLoginAttempts);
        }
    }
}

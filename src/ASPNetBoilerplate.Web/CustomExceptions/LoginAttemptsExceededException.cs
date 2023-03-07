namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Login Attempts ExceededException
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class LoginAttemptsExceededException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginAttemptsExceededException" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="loginLockDownMinutes">Login LockDown Minutes</param>
        /// <param name="lastLoginAttemptTime">Last Login Attempt Time</param>
        public LoginAttemptsExceededException(string userName, int loginLockDownMinutes, DateTime lastLoginAttemptTime)
            : base($"Login attempts exceeded for User: {userName}")
        {
            Data.Add("loginLockDownMinutes", loginLockDownMinutes);
            Data.Add("lastLoginAttemptTime", lastLoginAttemptTime);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginAttemptsExceededException" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="loginLockDownMinutes">Login LockDown Minutes</param>
        /// <param name="lastLoginAttemptTime">Last Login Attempt Time</param>
        /// <param name="innerException">The inner exception.</param>
        public LoginAttemptsExceededException(string userName, int loginLockDownMinutes, DateTime lastLoginAttemptTime, Exception innerException)
            : base($"Login attempts exceeded for User: {userName}", innerException)
        {
            Data.Add("loginLockDownMinutes", loginLockDownMinutes);
            Data.Add("lastLoginAttemptTime", lastLoginAttemptTime);
        }
    }
}

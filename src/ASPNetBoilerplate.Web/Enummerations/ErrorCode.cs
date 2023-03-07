namespace ASPNetBoilerplate.Web.Enummerations
{
    /// <summary>
    /// Contain all enum related to Error Codes
    /// </summary>
    public enum ErrorCode
    {
        // 0 - 49 Unknown Exceptions, 50 - 100 Auth Exception and 100 - 200 Controller Exception

        /// <summary>
        /// Indicates an unkown error on the server
        /// </summary>
        UnknownError = 0,

        #region Internal Server errors

        /// <summary>
        /// Indicates a server error while processing the request
        /// </summary>
        InternalServerError = 1,

        #endregion Internal Server errors

        #region Authentication Exceptions

        /// <summary>
        /// Invalid Login Credentials
        /// </summary>
        InvalidLoginCredentials = 50,

        /// <summary>
        /// The login attempts exceeded
        /// </summary>
        LoginAttemptsExceeded = 51,

        /// <summary>
        /// The token malformed exception
        /// </summary>
        MalformedJWTokenReceivedException = 52,

        /// <summary>
        /// The token expired exception
        /// </summary>
        TokenExpiredException = 53,

        #endregion Authentication Exceptions

        #region Custom errors

        /// <summary>
        /// Indicates that the user creation failed because the user already exists
        /// </summary>
        EntityAlreadyExists = 101,

        /// <summary>
        /// Indicates that the syntax of the request data is invalid
        /// </summary>
        InvalidRequestData = 102,

        /// <summary>
        /// Indicates that entity required does not exist
        /// </summary>
        EntityDoesNotExist = 103,

        /// <summary>
        /// Indicates that the entities were not found
        /// </summary>
        EntitiesNotFoundException = 104,

        /// <summary>
        /// DataBaseUpdateFailureException failure Error
        /// </summary>
        DataBaseUpdateFailureException = 105,

        /// <summary>
        /// Indicates a HTTP exception occurred.
        /// </summary>
        HttpException = 107,

        /// <summary>
        /// The requested operation/feature unsupported
        /// </summary>
        UnsupportedOperation = 108,

        /// <summary>
        /// The user mismatch exception
        /// </summary>
        UserMismatchException = 109,

        #endregion Custom errors
    }
}

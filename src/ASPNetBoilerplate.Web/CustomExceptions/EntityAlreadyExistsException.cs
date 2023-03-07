namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Represents an exception which indicates that the entity to be created already exists
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EntityAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException" /> class.
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="parameter">The parameter used to find the entity</param>
        public EntityAlreadyExistsException(Type entityType, string parameter)
            : base($"A {entityType?.Name} with the given {parameter} already exists")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException" /> class.
        /// </summary>
        /// <param name="message">The custom Exception messge</param>
        /// <param name="innerException">The inner exception of the thrown exception</param>
        public EntityAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

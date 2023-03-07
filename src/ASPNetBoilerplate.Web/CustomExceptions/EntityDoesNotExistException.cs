using System.Reflection;

namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Represents an exception which indicates that the entity required does not exist
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EntityDoesNotExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistException" /> class.
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="parameter">The parameter used to find the entity</param>
        public EntityDoesNotExistException(MemberInfo entityType, string parameter)
            : base($"A {entityType?.Name} with the given {parameter} does not exist")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistException" /> class.
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="parameter">The parameter used to find the entity</param>
        /// <param name="value">The value of the parameter</param>
        public EntityDoesNotExistException(MemberInfo entityType, string parameter, string value)
            : base($"A {entityType?.Name} with the given {parameter} '{value}' does not exist")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistException" /> class.
        /// </summary>
        /// <param name="message">The custom Exception messge</param>
        /// <param name="innerException">The inner exception of the thrown exception</param>
        public EntityDoesNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistException" /> class.
        /// </summary>
        /// <param name="message">The custom Exception messge</param>
        public EntityDoesNotExistException(string message)
            : base(message)
        {
        }
    }
}

using System.Reflection;

namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Represents the class for the exception which is thrown when property of the reques data model is invalid or not given
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidRequestDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestDataException" /> class.
        /// </summary>
        /// <param name="entityType">The type of entity</param>
        /// <param name="propertyName">The invalid property name</param>
        public InvalidRequestDataException(MemberInfo entityType, string propertyName)
            : base($"The {entityType?.Name}'s {propertyName} is either not given or invalid")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestDataException" /> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="additionalInfo">The additional information.</param>
        public InvalidRequestDataException(MemberInfo entityType, string propertyName, string additionalInfo)
            : base($"The {entityType?.Name}'s {propertyName} is either not given or invalid. {additionalInfo}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestDataException" /> class.
        /// </summary>
        /// <param name="parameterName">The name of the parameter</param>
        public InvalidRequestDataException(string parameterName)
            : base($"The {parameterName} is either not given or invalid")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestDataException" /> class.
        /// </summary>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="value">The value of the parameter.</param>
        public InvalidRequestDataException(string parameterName, string value)
            : base($"The provided value '{value}' for the parameter {parameterName} is invalid")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestDataException" /> class.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="additionalInfo">The additional information about error.</param>
        public InvalidRequestDataException(string value, string parameterName, string additionalInfo)
            : base($"The provided value '{value}' for the parameter {parameterName} is invalid. {additionalInfo}")
        {
        }
    }
}

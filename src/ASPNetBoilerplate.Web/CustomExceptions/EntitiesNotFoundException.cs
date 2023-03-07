using System.Reflection;

namespace ASPNetBoilerplate.Web.CustomExceptions
{
    /// <summary>
    /// Represents the exception class thrown when entities of a given parameter are not found
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EntitiesNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesNotFoundException" /> class.
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="parameter">The parameter used to find the entity</param>
        /// <param name="values">Comma separated List of values</param>
        public EntitiesNotFoundException(MemberInfo entityType, string parameter, string values)
            : base($"{entityType?.Name}s with the {parameter}s '{values}' were not found")
        {
        }
    }
}

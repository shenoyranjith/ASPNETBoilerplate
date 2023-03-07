using ASPNetBoilerplate.Domain.Entities;
using DapperExtensions.Mapper;

namespace ASPNetBoilerplate.Repository.DapperClassMappers
{
    /// <summary>
    /// A Dapper class mapper for User entity
    /// </summary>
    /// <seealso cref="DapperExtensions.Mapper.ClassMapper&lt;ASPNetBoilerplate.Domain.Entities.User&gt;" />
    public sealed class UserMapper : ClassMapper<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapper" /> class.
        /// </summary>
        public UserMapper()
        {
            Table("Users");
            AutoMap();
        }
    }
}

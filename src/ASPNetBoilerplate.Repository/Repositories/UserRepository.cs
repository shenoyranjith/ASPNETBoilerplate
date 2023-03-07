using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Domain.Interfaces;
using ASPNetBoilerplate.Repository.Generic;
using ASPNetBoilerplate.Repository.Repositories.Interfaces;

namespace ASPNetBoilerplate.Repository.Repositories
{
    /// <summary>
    ///   <inheritdoc cref="IUserRepository" />
    /// </summary>
    /// <seealso cref="ASPNetBoilerplate.Repository.Generic.GenericRepository&lt;ASPNetBoilerplate.Domain.Entities.User&gt;" />
    /// <seealso cref="ASPNetBoilerplate.Repository.Repositories.Interfaces.IUserRepository" />
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="coreConnectionResolver"><inheritdoc cref="ICoreConnectionResolver" /></param>
        public UserRepository(IConnectionResolver coreConnectionResolver)
            : base(coreConnectionResolver)
        {
        }
    }
}
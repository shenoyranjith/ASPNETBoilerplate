using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Repository.Generic.Interfaces;

namespace ASPNetBoilerplate.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Contains methods to access User information from database
    /// </summary>
    /// <seealso cref="ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository&lt;ASPNetBoilerplate.Domain.Entities.User&gt;" />
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}

using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Domain.Enumerations;
using ASPNetBoilerplate.Web.Models.ClientModels.User;
using ASPNetBoilerplate.Web.Models.Filters;
using Microsoft.AspNetCore.JsonPatch;

namespace ASPNetBoilerplate.Web.Services.Interfaces
{
    /// <summary>
    /// Handles user functionality
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Validates the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="isUpdate">if set to <c>true</c> [is update].</param>
        /// <returns>
        ///   <see langword="true" />if the user model is valid. Otherwise returns false.
        /// </returns>
        bool Validate(string username, string firstName, string email, string password, bool isUpdate = false);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// User with given id.
        /// </returns>
        User Get(int id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        /// <returns>
        /// List of users matching the request.
        /// </returns>
        IEnumerable<User> GetAll(UserRequest userRequest);

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userRole">The user role.</param>
        /// <returns>
        /// The new user.
        /// </returns>
        User Add(CreateUserRequest user, UserRole userRole);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The updated user.
        /// </returns>
        User Update(int id, UpdateUserRequest user);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The deleted user.
        /// </returns>
        User Delete(int id);

        /// <summary>
        /// Counts the specified user filter.
        /// </summary>
        /// <param name="userFilter">The user filter.</param>
        /// <returns>
        /// The count of users matching the request.
        /// </returns>
        long Count(UserFilter userFilter);
    }
}

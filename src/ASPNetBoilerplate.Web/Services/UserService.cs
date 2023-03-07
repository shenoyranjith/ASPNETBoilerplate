using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Domain.Enumerations;
using ASPNetBoilerplate.Repository.Repositories.Interfaces;
using ASPNetBoilerplate.Web.CustomExceptions;
using ASPNetBoilerplate.Web.Models.ClientModels.User;
using ASPNetBoilerplate.Web.Models.Filters;
using ASPNetBoilerplate.Web.Services.Interfaces;
using AutoMapper;
using DapperExtensions;
using System.Text.RegularExpressions;

namespace ASPNetBoilerplate.Web.Services
{
    /// <summary>
    /// The user service
    /// </summary>
    /// <seealso cref="ASPNetBoilerplate.Web.Services.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The cryptography service
        /// </summary>
        private readonly ICryptographyService _cryptographyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="cryptographyService">The cryptography service.</param>
        public UserService(IUserRepository userRepository, ICryptographyService cryptographyService)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
        }

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userRole"></param>
        /// <returns>
        /// The new user.
        /// </returns>
        public User Add(CreateUserRequest user, UserRole userRole)
        {
            var newUser = new User()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = _cryptographyService.ComputeSha256Hash(user.Password),
                UserRole = userRole,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _userRepository.Add(newUser);
            return newUser;
        }

        /// <summary>
        /// Counts the specified user filter.
        /// </summary>
        /// <param name="userFilter">The user filter.</param>
        /// <returns>
        /// The count of users matching the request.
        /// </returns>
        public long Count(UserFilter userFilter)
        {
            if(userFilter == null)
            {
                return _userRepository.Count(Predicates.Field<User>(u => u.Id, Operator.Gt, 0));
            }
            else
            {
                return _userRepository.CountByPredicateGroup(GenerateUserPredicate(userFilter));
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The deleted user.
        /// </returns>
        /// <exception cref="ASPNetBoilerplate.Web.CustomExceptions.EntityDoesNotExistException">id</exception>
        public User Delete(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new EntityDoesNotExistException(typeof(User), nameof(id));
            }
            else
            {
                _userRepository.Delete(id);
                return user;
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// User with given id.
        /// </returns>
        public User Get(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new EntityDoesNotExistException(typeof(User), nameof(id));
            }
            return user;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        /// <returns>
        /// List of users matching the request.
        /// </returns>
        public IEnumerable<User> GetAll(UserRequest userRequest)
        {
            if (userRequest != null)
            {
                _userRepository.SetPageLimit(userRequest.PageSize);
                _userRepository.SetPageOffset(userRequest.PageNumber - 1);
            }
            if (userRequest != null & userRequest.Filter != null)
            {
                PredicateGroup predicateGroup = GenerateUserPredicate(userRequest.Filter);

                return _userRepository.GetByPredicate(predicateGroup);
            }
            else
            {
                return _userRepository.GetAll();
            }
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The updated user.
        /// </returns>
        /// <exception cref="ASPNetBoilerplate.Web.CustomExceptions.EntityDoesNotExistException">id</exception>
        /// <exception cref="ASPNetBoilerplate.Web.CustomExceptions.InvalidRequestDataException">UpdateUserRequest</exception>
        public User Update(int id, UpdateUserRequest user)
        {
            var userToUpdate = _userRepository.Get(id);
            if(userToUpdate == null)
            {
                throw new EntityDoesNotExistException(typeof(User), nameof(id));
            }
            Validate(user.Username, user.FirstName, user.EmailAddress, user.Password, true);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateUserRequest, User>()
                .ForMember(
                    user => user.Password,
                    m => m.MapFrom(usr => 
                        (usr.Password != null) ? _cryptographyService.ComputeSha256Hash(usr.Password) : null))
                .ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null)));
            var mapper = new Mapper(config);
            mapper.Map(user, userToUpdate);
            userToUpdate.UpdatedAt = DateTime.UtcNow;
            _userRepository.Update(userToUpdate);
            return userToUpdate;
        }

        /// <summary>
        /// Validates the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="emailAddress">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="isUpdate"></param>
        /// <returns>
        ///   <see langword="true" />if the user model is valid. Otherwise returns false.
        /// </returns>
        /// <exception cref="System.ArgumentException">Username is required - username
        /// or
        /// Username is already taken - username
        /// or
        /// First Name is required - firstName
        /// or
        /// Email Address is required - email
        /// or
        /// Email Address is invalid - email
        /// or
        /// Password is required - password
        /// or
        /// The entered password doesn't meet the requirements - password</exception>
        public bool Validate(string username, string firstName, string emailAddress, string password, bool isUpdate = false)
        {
            if (!isUpdate && string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is required.", nameof(username));
            }
            else if (!string.IsNullOrEmpty(username))
            {
                var existingUser = GetAll(
                    new UserRequest() { Filter = new UserFilter() { Username = username } }).FirstOrDefault();
                if (existingUser != null)
                {
                    throw new ArgumentException("Username is already taken.", nameof(username));
                }
            }
            if (!isUpdate && string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("First Name is required.", nameof(firstName));
            }
            if (!isUpdate && string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Email Address is required.", nameof(emailAddress));
            }
            else if (!string.IsNullOrEmpty(password) && !Regex.IsMatch(emailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                throw new ArgumentException("Email Address is invalid.", nameof(emailAddress));
            }
            else if (!string.IsNullOrEmpty(emailAddress))
            {
                var existingUser = GetAll(
                    new UserRequest() { Filter = new UserFilter() { EmailAddress = emailAddress } }).FirstOrDefault();
                if (existingUser != null)
                {
                    throw new ArgumentException("Email address is already in use.", nameof(emailAddress));
                }
            }
            if (!isUpdate && string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is required", nameof(password));
            }
            else if (!string.IsNullOrEmpty(password) && !Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
            {
                throw new ArgumentException("The entered password doesn't meet the requirements.", nameof(password));
            }
            return true;
        }

        /// <summary>
        /// Generates the user predicate.
        /// </summary>
        /// <param name="userFilter">The user filter.</param>
        /// <returns></returns>
        private PredicateGroup GenerateUserPredicate(UserFilter userFilter)
        {
            var predicateGroup = new PredicateGroup()
            {
                Predicates = new List<IPredicate>()
            };
            if (userFilter.Username != null)
            {
                predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.Username, Operator.Eq, userFilter.Username));
            }
            if (userFilter.FirstName != null)
            {
                predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.FirstName, Operator.Eq, userFilter.FirstName));
            }
            if (userFilter.LastName != null)
            {
                predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.LastName, Operator.Eq, userFilter.LastName));
            }
            if (userFilter.EmailAddress != null)
            {
                predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.EmailAddress, Operator.Eq, userFilter.EmailAddress));
            }
            if (userFilter.UserRole != null)
            {
                predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserRole, Operator.Eq, userFilter.UserRole));
            }

            return predicateGroup;
        }
    }
}

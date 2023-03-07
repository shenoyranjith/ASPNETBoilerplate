using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Web.Models.ClientModels.Authentication;
using ASPNetBoilerplate.Web.Services.Interfaces;
using ASPNetBoilerplate.Web.Models.Configuration;
using Microsoft.Extensions.Options;
using DapperExtensions;
using ASPNetBoilerplate.Web.CustomExceptions;
using System.Globalization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ASPNetBoilerplate.Repository.Repositories.Interfaces;

namespace ASPNetBoilerplate.Web.Services
{
    /// <summary>
    ///   <inheritdoc cref="ICustomAuthenticationService" />
    /// </summary>
    /// <seealso cref="ASPNetBoilerplate.Web.Services.Interfaces.ICustomAuthenticationService" />
    public class CustomAuthenticationService : ICustomAuthenticationService
    {

        /// <summary>
        /// The authentication options
        /// </summary>
        private readonly IOptions<AuthOptions> _authOptions;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;


        /// <summary>
        /// The cryptography service
        /// </summary>
        private readonly ICryptographyService _cryptographyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthenticationService" /> class.
        /// </summary>
        /// <param name="authOptions">The authentication options.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="cryptographyService">The cryptography service.</param>
        public CustomAuthenticationService(
            IOptions<AuthOptions> authOptions,
            IUserRepository userRepository,
            ICryptographyService cryptographyService)
        {
            _authOptions = authOptions;
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
        }


        /// <summary>
        /// Authenticates the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>
        /// Validated User
        /// </returns>
        /// <exception cref="System.ArgumentNullException">login</exception>
        /// <exception cref="ASPNetBoilerplate.Web.CustomExceptions.InvalidCredentialsException"></exception>
        public User Authenticate(Login login)
        {
            if (login is null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            var errorMessage = string.Empty;
            login.Username = login.Username.Trim();
            login.Password = login.Password.Trim();
            var currentUser =
            _userRepository.GetByPredicate(Predicates.Field<User>(u => u.Username, Operator.Eq, login.Username)).FirstOrDefault();

            if (currentUser == null)
            {
                throw new InvalidCredentialsException();
            }

            CheckCurrentLoginAttempt(currentUser);

            if (!IsValidPassword(currentUser, login.Password))
            {
                IncrementLoginAttempts(currentUser);
                throw new InvalidCredentialsException(_authOptions.Value.MaxLoginAttempts, currentUser.LoginAttempts ?? 0);
            }

            return currentUser;
        }

        /// <summary>
        /// Builds the authenticated token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// JWT Token
        /// </returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public string BuildAuthenticatedToken(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return BuildJwtToken(user, tokenExpiryDays: 30, authenticatedClaim: true);
        }

        /// <summary>
        /// Checks the current login attempt.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="ASPNetBoilerplate.Web.CustomExceptions.LoginAttemptsExceededException"></exception>
        private void CheckCurrentLoginAttempt(User user)
        {
            if (IsLoginAttemptsExceeded(user) && IsUnlockPeriodPassed(user))
            {
                ResetLoginAttempts(user);
            }
            if (IsLoginAttemptsExceeded(user) && !IsUnlockPeriodPassed(user))
            {
                throw new LoginAttemptsExceededException(user.Username, _authOptions.Value.LoginLockDownMinutes, user.UpdatedAt);
            }
        }

        /// <summary>
        /// Determines whether [is login attempts exceeded] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if [is login attempts exceeded] [the specified user]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsLoginAttemptsExceeded(User user)
        {
            return user.LoginAttempts >= _authOptions.Value.MaxLoginAttempts;
        }

        private bool IsUnlockPeriodPassed(User user)
        {
            return (DateTime.UtcNow - user.UpdatedAt).TotalMinutes > _authOptions.Value.LoginLockDownMinutes;
        }

        /// <summary>
        /// Resets the login attempts.
        /// </summary>
        /// <param name="user">The user.</param>
        private void ResetLoginAttempts(User user)
        {
            user.LoginAttempts = 0;
            user.UpdatedAt = DateTime.UtcNow;
            _userRepository.Update(user);
        }

        /// <summary>
        /// Determines whether [is valid password] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c> if [is valid password] [the specified user]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidPassword(User user, string password)
        {
            return user.Password != null && user.Password.Equals(_cryptographyService.ComputeSha256Hash(password), StringComparison.Ordinal);
        }

        /// <summary>
        /// Increments the login attempts.
        /// </summary>
        /// <param name="user">The user.</param>
        private void IncrementLoginAttempts(User user)
        {
            user.LoginAttempts = user.LoginAttempts + 1 ?? 1;
            user.UpdatedAt = DateTime.UtcNow;
            _userRepository.Update(user);
        }

        /// <summary>
        /// Builds the JWT token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="tokenExpiryDays">The token expiry days.</param>
        /// <param name="authenticatedClaim">if set to <c>true</c> [authenticated claim].</param>
        /// <returns>
        /// JWT Token
        /// </returns>
        private string BuildJwtToken(User user, int tokenExpiryDays, bool authenticatedClaim)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.UserData, user.Id.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Authentication, authenticatedClaim.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.UserData, user.UserRole.ToString()),
            }.ToList();

            return GenerateJWTToken(claims, DateTime.UtcNow.AddDays(tokenExpiryDays));
        }

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <param name="tokenExpiryTime">The token expiry time.</param>
        /// <returns>
        /// JWT Token
        /// </returns>
        private string GenerateJWTToken(IList<Claim> claims, DateTime tokenExpiryTime)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_authOptions.Value.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(_authOptions.Value.Issuer, null, claims, expires: tokenExpiryTime, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

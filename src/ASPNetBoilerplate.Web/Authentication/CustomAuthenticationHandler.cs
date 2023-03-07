using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text.Encodings.Web;
using System.Text;
using ASPNetBoilerplate.Web.Models.Configuration;
using ASPNetBoilerplate.Web.CustomExceptions;

namespace ASPNetBoilerplate.Web.Authentication
{
    /// <summary>
    /// Custom Authentication Handler
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authentication.AuthenticationHandler&lt;Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions&gt;" />
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// The application settings
        /// </summary>
        private readonly IOptions<AppSettings> _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthenticationHandler" /> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="logger">logger</param>
        /// <param name="encoder">encoder</param>
        /// <param name="clock">clock</param>
        /// <param name="appSettings">appsettigns</param>
        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<AppSettings> appSettings)
            : base(options, logger, encoder, clock)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Handle authentications.
        /// </summary>
        /// <returns>
        /// The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticateResult" />.
        /// </returns>
        /// <exception cref="MalformedJWTokenReceivedException"></exception>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var authorizationHeaderToken = Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorizationHeaderToken))
                {
                    return AuthenticateResult.NoResult();
                }
                else
                {
                    return ValidateToken(authorizationHeaderToken);
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
            catch (AuthenticationException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                if (ex.Message.Contains("must have three segments (JWS) or five segments (JWE).", StringComparison.InvariantCulture))
                {
                    throw new MalformedJWTokenReceivedException();
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="authorizationHeaderToken">The authorization header token.</param>
        /// <returns></returns>
        private AuthenticateResult ValidateToken(string? authorizationHeaderToken)
        {
            if (authorizationHeaderToken == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            var token = authorizationHeaderToken.Substring("bearer".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _appSettings.Value.AuthOptions.Issuer,
                IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_appSettings.Value.AuthOptions.SecurityKey))
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            if (validatedToken == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
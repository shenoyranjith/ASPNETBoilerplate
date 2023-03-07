using ASPNetBoilerplate.Web.Services.Interfaces;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ASPNetBoilerplate.Web.Services
{
    /// <summary>
    /// Cryptography Service
    /// </summary>
    /// <seealso cref="ASPNetBoilerplate.Web.Services.Interfaces.ICryptographyService" />
    /// <seealso cref="ICryptographyService" />
    public class CryptographyService : ICryptographyService
    {
        /// <summary>
        /// Computes the MD5 hash.
        /// </summary>
        /// <param name="input">String to be encrypted</param>
        /// <param name="guid">Gobal unique id</param>
        /// <returns>
        /// MD5 Hash
        /// </returns>
        public string ComputeMd5Hash(string input, string guid = null)
        {

            var hashInput = guid != null ? input + guid : input;

            var data = MD5.HashData(Encoding.Default.GetBytes(hashInput));

            var stringBuilder = new StringBuilder();
            foreach (var b in data)
            {
                stringBuilder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Computes the sha256 hash.
        /// </summary>
        /// <param name="input">String to be encrypted</param>
        /// <returns>
        /// SHA256 value
        /// </returns>
        public string ComputeSha256Hash(string input)
        {
            var stringBuilder = new StringBuilder();
            var result = SHA256.HashData(Encoding.UTF8.GetBytes(input));

            foreach (var b in result)
            {
                stringBuilder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Computes the hmac sha256 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// HMAC-SHA256 hash
        /// </returns>
        public string ComputeHmacSha256Hash(string input, string key)
        {
            var hash = new StringBuilder();
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var result = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (var b in result)
                {
                    hash.Append(b.ToString("x2", CultureInfo.InvariantCulture));
                }

                return hash.ToString();
            }
        }

        /// <summary>
        /// Generates the random token.
        /// </summary>
        /// <returns>
        /// Random Token
        /// </returns>
        public string GenerateRandomToken()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
    }
}

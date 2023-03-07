namespace ASPNetBoilerplate.Web.Services.Interfaces
{
    /// <summary>
    /// Contains Methods related to Cryptography
    /// </summary>
    public interface ICryptographyService
    {
        /// <summary>
        /// Computes the MD5 hash.
        /// </summary>
        /// <param name="input">String to be encrypted</param>
        /// <param name="guid">Global uniqu id</param>
        /// <returns>
        /// MD5 Hash Value
        /// </returns>
        string ComputeMd5Hash(string input, string guid = null);

        /// <summary>
        /// Computes the sha256 hash.
        /// </summary>
        /// <param name="input">String to be encrypted</param>
        /// <returns>
        /// SHA256 value
        /// </returns>
        string ComputeSha256Hash(string input);

        /// <summary>
        /// Computes the hmac sha256 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// HMAC-SHA256 hash
        /// </returns>
        string ComputeHmacSha256Hash(string input, string key);

        /// <summary>
        /// Generates the random token.
        /// </summary>
        /// <returns>
        /// Random Token
        /// </returns>
        string GenerateRandomToken();
    }
}

using System.Security.Cryptography;
using System.Text;

namespace NamazTimeApp.Core.Helpers
{
    public static class SHAHasher
    {
        /// <summary>
        /// Create hash for password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Hashed password.</returns>
        public static string CreateHash(string password)
        {
            using var sha512 = SHA512.Create();
            var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(bytes);
        }
    }
}

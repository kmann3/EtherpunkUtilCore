using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

/// <summary>
/// Etherpunk Utility Library
/// </summary>
namespace EtherpunkUtil
{
    /// <summary>
    /// Misc classes that don't belong anywhere special.
    /// </summary>
    public class Misc
    {
        /// <summary>
        /// Generates a random unique code given a prefix and a length.
        /// Certain numbers and letters are excluded the list is as follows:
        /// 2346789ABCDFGHJKMQRTVWXY
        /// This code is not terribly smart and WILL end with a '-' at the end if you specific a certain length where that can happen.
        /// </summary>
        /// <param name="uniqueId">Prefix (e.g. EPIM)</param>
        /// <param name="length">Length to generate.</param>
        /// <returns>Returns a randomly generated code.</returns>
        public static string GenerateNewUId(string uniqueId, int length) // generates a unique, random, and alphanumeric token
        {
            const string availableChars = "2346789ABCDFGHJKMQRTVWXY";
            using (var generator = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[length];
                generator.GetBytes(bytes);
                var chars = bytes.Select(b => availableChars[b % availableChars.Length]);
                var token = new string(chars.ToArray());

                token = Regex.Replace(token, ".{3}", "$0-").Trim('-');

                return uniqueId + token;
            }
        }

        /// <summary>
        /// Hashes a password in the same way Identity hashes.
        /// </summary>
        /// <param name="password">Password to hash. If null, will throw an exception.</param>
        /// <returns>Returns hash of password given.</returns>
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}

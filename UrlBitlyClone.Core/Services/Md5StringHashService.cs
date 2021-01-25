using System.Security.Cryptography;
using System.Text;
using UrlBitlyClone.Core.Services.Interfaces;

namespace UrlBitlyClone.Core.Services
{
    /// <summary>
    /// Hashes the string by creating an MD5 hash of it, then takes the first 8 characters of that hash.
    /// </summary>
    public class Md5StringHashService : IStringHashService
    {
        /// <inheritdoc/>
        public string HashUrl(string url)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(url);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < 4; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}

namespace UrlBitlyClone.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for hashing a string.
    /// </summary>
    public interface IStringHashService
    {
        /// <summary>
        /// Takes an input string, hashes it then returns the hash.
        /// </summary>
        /// <param name="url">The url to hash.</param>
        /// <returns>
        /// A string containing the hash.
        /// </returns>
        string HashUrl(string url);
    }
}

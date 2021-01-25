using UrlBitlyClone.Core.Services.Interfaces;

namespace UrlBitlyClone.Core.Services
{
    /// <summary>
    /// Implementation of <see cref="IStringHashService"/> that doesn't actually hash the string.
    /// </summary>
    /// <remarks>
    /// Purely for testing with a real database.
    /// </remarks>
    public class NoneStringHashService : IStringHashService
    {
        /// <inheritdoc/>
        public string HashUrl(string url)
        {
            return url;
        }
    }
}

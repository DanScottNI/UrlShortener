using UrlBitlyClone.Core.Context;

namespace UrlBitlyClone.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for the url shortening.
    /// </summary>
    public interface IUrlShortenerService
    {
        /// <summary>
        /// Creates a new shortened URL.
        /// </summary>
        /// <param name="url">The URL to shorten.</param>
        /// <returns>
        /// An <see cref="UrlShortening"/> object that represents the newly shortened URL entity.
        /// </returns>
        UrlShortening Create(string url);

        /// <summary>
        /// Gets the URL information by its short form url.
        /// </summary>
        /// <param name="shortenedUrl">The shortened URL.</param>
        /// <returns>
        /// An <see cref="UrlShortening"/> object that matches the short form url.
        /// </returns>
        UrlShortening GetByShortUrl(string shortenedUrl);
    }
}

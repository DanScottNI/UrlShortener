#nullable disable

namespace UrlBitlyClone.Core.Context
{
    /// <summary>
    /// Entity for the UrlShortening table.
    /// </summary>
    public class UrlShortening
    {
        /// <summary>
        /// Gets or sets the URL shortening identifier.
        /// </summary>
        public long UrlShorteningId { get; set; }

        /// <summary>
        /// Gets or sets the full URL.
        /// </summary>
        public string FullUrl { get; set; }

        /// <summary>
        /// Gets or sets the shortened URL.
        /// </summary>
        public string ShortenedUrl { get; set; }
    }
}

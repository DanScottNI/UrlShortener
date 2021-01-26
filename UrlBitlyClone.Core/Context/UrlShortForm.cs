#nullable disable

namespace UrlBitlyClone.Core.Context
{
    /// <summary>
    /// Entity for the UrlShortForm table.
    /// </summary>
    public class UrlShortForm
    {
        /// <summary>
        /// Gets or sets the URL shortening identifier.
        /// </summary>
        public long UrlShortFormId { get; set; }

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

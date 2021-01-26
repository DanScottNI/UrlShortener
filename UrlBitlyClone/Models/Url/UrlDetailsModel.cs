namespace UrlBitlyClone.Models.Url
{
    /// <summary>
    /// Model for displaying the generated url information.
    /// </summary>
    public class UrlDetailsModel
    {
        /// <summary>
        /// Gets or sets the full original url.
        /// </summary>
        public string FullUrl { get; set; }

        /// <summary>
        /// Gets or sets the shortened URL.
        /// </summary>
        public string ShortenedUrl { get; set; }

        /// <summary>
        /// Gets or sets the base URL for the site.
        /// </summary>
        public string BaseUrl { get; set; }
    }
}

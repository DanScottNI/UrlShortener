using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UrlBitlyClone.Core.Services.Interfaces;

namespace UrlBitlyClone.Controllers
{
    /// <summary>
    /// API controller for URL actions.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/url")]
    [ApiController]
    public class ApiUrlController : ControllerBase
    {
        private readonly IUrlShortenerService urlShortenerService;

        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiUrlController"/> class.
        /// </summary>
        /// <param name="urlShortenerService">The URL shortener service.</param>
        /// <param name="configuration">An instance of configuration.</param>
        public ApiUrlController(IUrlShortenerService urlShortenerService, IConfiguration configuration)
        {
            this.urlShortenerService = urlShortenerService;
            this.configuration = configuration;
        }

        /// <summary>
        /// Creates the specified URL as a short-form URL.
        /// </summary>
        /// <param name="url">The URL to convert to short-form.</param>
        /// <returns>
        /// The complete short-form URL.
        /// </returns>
        [HttpPost]
        public string Create(string url)
        {
            var obj = this.urlShortenerService.Create(url);

            if (obj != null)
            {
                return this.configuration.GetValue<string>("BaseUrl").TrimEnd('\\').TrimEnd('/') + "/" + obj.ShortenedUrl;
            }

            return string.Empty;
        }
    }
}

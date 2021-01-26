using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UrlBitlyClone.Core.Services.Interfaces;
using UrlBitlyClone.Models.Url;

namespace UrlBitlyClone.Controllers
{
    /// <summary>
    /// Controller for URL actions.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UrlController : Controller
    {
        private readonly IUrlShortenerService urlShortenerService;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlController"/> class.
        /// </summary>
        /// <param name="urlShortenerService">The URL shortener service.</param>
        /// <param name="configuration">An instance of configuration.</param>
        public UrlController(IUrlShortenerService urlShortenerService, IConfiguration configuration)
        {
            this.urlShortenerService = urlShortenerService;
            this.configuration = configuration;
        }

        /// <summary>
        /// Displays the information about the specified URL.
        /// </summary>
        /// <param name="url">The short-form URL.</param>
        /// <returns>
        /// An HTML page containing the URL's information.
        /// </returns>
        [HttpGet("details/{url}")]
        public IActionResult Details(string url)
        {
            var obj = this.urlShortenerService.GetByShortUrl(url);

            if (obj != null)
            {
                UrlDetailsModel model = new UrlDetailsModel
                {
                    BaseUrl = this.configuration.GetValue("BaseUrl", string.Empty).TrimEnd('/').TrimEnd('\\'),
                    FullUrl = obj.FullUrl,
                    ShortenedUrl = obj.ShortenedUrl,
                };

                return this.View(model);
            }

            return this.RedirectToAction("Unknown");
        }

        /// <summary>
        /// Redirects the user to the specified URL using the short-form address.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// Redirects the user to the real URL behind the short-form one. Or if the short-form one doesn't exist, then to the Unknown action.
        /// </returns>
        [HttpGet("{url}")]
        public IActionResult RedirectToUrl(string url)
        {
            var obj = this.urlShortenerService.GetByShortUrl(url);

            if (obj != null)
            {
                return this.RedirectPermanent(obj.FullUrl);
            }

            return this.RedirectToAction("Unknown");
        }

        /// <summary>
        /// Displays the Unknown URL page.
        /// </summary>
        /// <returns>
        /// A View containing the Unknown URL page.
        /// </returns>
        [HttpGet("unknown")]
        public IActionResult Unknown()
        {
            return this.View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UrlBitlyClone.Core.Services.Interfaces;
using UrlBitlyClone.Models.Url;

namespace UrlBitlyClone.Controllers
{
    [Route("")]
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

        [HttpGet("unknown")]
        public IActionResult Unknown()
        {
            return this.View();
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Core.Services.Interfaces;
using UrlBitlyClone.Models;
using UrlBitlyClone.Models.Home;

namespace UrlBitlyClone.Controllers
{
    /// <summary>
    /// The home controller of the application.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUrlShortenerService urlShortenerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="urlShortenerService">The URL shortener service.</param>
        public HomeController(ILogger<HomeController> logger, IUrlShortenerService urlShortenerService)
        {
            this.logger = logger;
            this.urlShortenerService = urlShortenerService;
        }

        /// <summary>
        /// Displays the home page's HTML.
        /// </summary>
        /// <returns>
        /// A <see cref="ViewResult"/> with the home page's HTML.
        /// </returns>
        public IActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HomeIndexModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            UrlShortening url = this.urlShortenerService.Create(model.Url);

            this.logger.LogInformation("Logged as the following short-form URL {ShortenedUrl}", url.ShortenedUrl);
            return this.RedirectToAction("Details", "Url", new { url = url.ShortenedUrl });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

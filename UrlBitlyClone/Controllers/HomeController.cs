using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Core.Services.Interfaces;
using UrlBitlyClone.Models;
using UrlBitlyClone.Models.Home;

namespace UrlBitlyClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUrlShortenerService urlShortenerService;

        public HomeController(ILogger<HomeController> logger, IUrlShortenerService urlShortenerService)
        {
            this.logger = logger;
            this.urlShortenerService = urlShortenerService;
        }

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

            return this.RedirectToAction("Details", "Url", new { url = url.ShortenedUrl });
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;

namespace UrlBitlyClone.Controllers
{
    [Route("[action]")]
    public class UrlController : Controller
    {
        [HttpGet("details/{url}")]
        public IActionResult Details(string url)
        {
            return this.View();
        }

        [HttpGet("{url}")]
        public IActionResult RedirectToUrl(string url)
        {
            throw new NotImplementedException();
        }
    }
}

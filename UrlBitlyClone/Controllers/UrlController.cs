using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrlBitlyClone.Controllers
{
    public class UrlController : Controller
    {
        [HttpGet("details/{url}")]
        public IActionResult Details(string url)
        {
            return this.View();
        }
    }
}

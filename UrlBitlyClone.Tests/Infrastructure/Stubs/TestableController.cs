using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{
    public class TestableController<T> : ITestableController<T> where T : Controller
    {
        public TestableController(T controller, IHttpContextAccessor httpContextAccessor, IUrlHelper url)
        {
            Controller = controller;
            Controller.ControllerContext.HttpContext = httpContextAccessor.HttpContext;
            Controller.Url = url;

        }

        public T Controller { get; set; }
    }
}

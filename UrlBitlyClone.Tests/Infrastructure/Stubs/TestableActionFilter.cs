using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{
    public interface ITestableActionFilter<T, TController> where T : IActionFilter where TController : Controller
    {
        void OnActionExecuted(ActionExecutedContext context);
        ActionExecutedContext OnActionExecuted();
        void OnActionExecuting(ActionExecutingContext context);
        ActionExecutingContext OnActionExecuting();
    }

    public class TestableActionFilter<T, TController> : ITestableActionFilter<T, TController> where T : IActionFilter where TController : Controller
    {
        private readonly T filter;
        private readonly TController controller;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TestableActionFilter(T filter, TController controller, IHttpContextAccessor httpContextAccessor)
        {
            this.filter = filter;
            this.controller = controller;
            this.httpContextAccessor = httpContextAccessor;
        }

        public ActionExecutingContext OnActionExecuting()
        {
            ActionExecutingContext context = new ActionExecutingContext(new ActionContext(httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor() { DisplayName = "Index" }), new List<IFilterMetadata>(), new Dictionary<string, object>(), controller);
            this.OnActionExecuting(context);
            return context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            filter.OnActionExecuting(context);
        }

        public ActionExecutedContext OnActionExecuted()
        {
            ActionExecutedContext context = new ActionExecutedContext(new ActionContext(httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor() { DisplayName = "Index" }), new List<IFilterMetadata>(), controller);
            this.OnActionExecuted(context);
            return context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            filter.OnActionExecuted(context);
        }
    }
}

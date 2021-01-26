using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{
    public class FakeUrlHelperFactory : IUrlHelperFactory
    {
        public IUrlHelper GetUrlHelper(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
    public class FakeUrlHelper : IUrlHelper
    {
        public ActionContext ActionContext { get; }

        public string Action(UrlActionContext actionContext)
        {
            return TestConstants.TestSiteUri + "i/am/a/test/url";
        }

        public string Content(string contentPath)
        {
            throw new NotImplementedException();
        }

        public bool IsLocalUrl(string url)
        {
            throw new NotImplementedException();
        }

        public string Link(string routeName, object values)
        {
            throw new NotImplementedException();
        }

        public string RouteUrl(UrlRouteContext routeContext)
        {
            throw new NotImplementedException();
        }
    }
}

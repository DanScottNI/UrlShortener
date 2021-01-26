using Microsoft.AspNetCore.Mvc;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{
    public interface ITestableController<T> where T : Controller
    {
        public T Controller { get; set; }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{
    public static    class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAspNetStubs(this IServiceCollection services)
        {
            services
                .AddScoped<IHttpContextAccessor, FakeHttpContextAccessor>(x => new FakeHttpContextAccessor(x))
                .AddSingleton<IWebHostEnvironment, FakeIHostingEnvironment>()
                .AddScoped<IUrlHelperFactory, FakeUrlHelperFactory>()
                .AddScoped<IUrlHelper, FakeUrlHelper>()
                .AddLogging(x => x.AddProvider(NullLoggerProvider.Instance))
                .AddScoped(typeof(ITestableController<>), typeof(TestableController<>))
                .AddScoped(typeof(ITestableActionFilter<,>), typeof(TestableActionFilter<,>));
            return services;
        }
    }
}

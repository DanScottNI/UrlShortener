using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Core.Extensions;
using UrlBitlyClone.Tests.Infrastructure;

namespace UrlBitlyClone.Tests
{
    /// <summary>
    /// Startup class for configuring IoC within the tests.
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)

        {
            // Set up the context. This context is recreated at the start of every test.
            services.AddDbContext<UrlBitlyCloneContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()), optionsLifetime: ServiceLifetime.Scoped);

            services.AddScoped(typeof(ObjectMother));

            services.AddUrlShortenerLibraries(Core.Enums.StringHashTypes.TruncatedMd5);

        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlBitlyClone.Controllers;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Core.Extensions;
using UrlBitlyClone.Infrastructure.ActionFilters;
using UrlBitlyClone.Tests.Infrastructure;
using UrlBitlyClone.Tests.Infrastructure.Stubs;

namespace UrlBitlyClone.Tests
{
    /// <summary>
    /// Startup class for configuring IoC within the tests.
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped((Func<IServiceProvider, IConfiguration>)(provider => new ConfigurationBuilder()
              .AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "BaseUrl", "http://danstest.ly/" },
            }).Build()));

            // Set up the context. This context is recreated at the start of every test.
            services.AddDbContext<UrlBitlyCloneContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()), optionsLifetime: ServiceLifetime.Scoped);

            services.AddAspNetStubs();
            services.AddScoped(typeof(ObjectMother));

            services.AddUrlShortenerLibraries(Core.Enums.StringHashTypes.TruncatedMd5);

            services.AddMvc()
                .AddControllerDifferentAssembly<HomeController>()
                .AddActionFiltersDifferentAssembly<UpdateSchemaFilter>();

        }
    }
}

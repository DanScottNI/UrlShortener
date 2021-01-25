using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace UrlBitlyClone.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)

        {
            // Set up DB connection string
            //services.AddDbContext<LandsTribunalContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()), optionsLifetime: ServiceLifetime.Scoped);

        }
    }
}

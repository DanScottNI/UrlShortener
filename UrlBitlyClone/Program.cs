using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace UrlBitlyClone
{
    /// <summary>
    /// The main program class.
    /// </summary>
    public class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>An IHostBuilder instance with setup.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .MinimumLevel.Override("Microsoft.EntityFramework", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.MSSqlServer(hostingContext.Configuration.GetConnectionString("DefaultConnection"), new MSSqlServerSinkOptions()
                    {
                        TableName = "Serilog",
                        SchemaName = "Logging",
                        AutoCreateSqlTable = false,
                    }));
    }
}

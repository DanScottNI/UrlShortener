using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Core.Enums;
using UrlBitlyClone.Core.Extensions;
using UrlBitlyClone.DatabaseMigrations.Migrations;
using UrlBitlyClone.Infrastructure.ActionFilters;

namespace UrlBitlyClone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Grab the connection string to the database.
            var defaultConnection = this.Configuration.GetConnectionString("DefaultConnection");
            var hashTypes = this.Configuration.GetValue<StringHashTypes>("StringHashType");
            var hasInProcessMigrations = this.Configuration.GetValue<bool>("UseInProcessMigrations", true);

            // We're using the in-memory database runner for pure use of ease.
            services.AddFluentMigratorCore().ConfigureRunner(x => x
                .AddSqlServer()
                .WithGlobalConnectionString(defaultConnection)
                .ScanIn(typeof(_20210125_AddUrlsTables).Assembly));

            services.AddUrlShortenerLibraries(hashTypes);

            // Set up DB connection string
            services.AddDbContext<UrlBitlyCloneContext>(options =>
            {
                options.UseSqlServer(defaultConnection);
            });

            // If migrations are set to run in process, inject the action filter that runs them on every request.
            if (hasInProcessMigrations)
            {
                services.AddControllersWithViews()
                    .AddMvcOptions(x => x.Conventions.Add(new UpdateSchemaFilterControllerConvention()));
            }
            else
            {
                services.AddControllersWithViews();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
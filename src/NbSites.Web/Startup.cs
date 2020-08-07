using Common;
using Common.AppContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NbSites.Web.Boots;

namespace NbSites.Web
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly IHostingEnvironment _env;

        public Startup(ILogger<Startup> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMyServiceLocator();
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMyStaticFiles(_env, _logger);
            
            //do not forget to add[Area("Foo")] in controllers of area
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "route_area",
                    template: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { site = "default", user = (string)null },
                    constraints: null,
                    dataTokens: new
                    {
                        route = "route_area"
                    });

                //todo redirect default "/" by config from HomeController
                routes.MapRoute(
                    name: "route_root",
                    template: "{controller=Home}/{action=Index}/{id?}",
                    defaults: new { site = "default", user = (string)null },
                    constraints: null,
                    dataTokens: new
                    {
                        route = "route_root",
                        area = (string)null
                    });
            });

            var myAppContext = MyAppContext.Current;
            myAppContext.SetBagValue("EntryUri", "~/Portal/Home/Index");

        }
    }
}

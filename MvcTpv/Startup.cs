using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MvcTpv
{
    using Microsoft.EntityFrameworkCore;
    // using Models;
    using Data;
    using System.Globalization;
    using Microsoft.AspNetCore.Localization;
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
                services.AddDbContext<MvcTpvContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MvcTpvConnection")));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            //Culuture
            //var defaultCulture = new CultureInfo("es-ES");

            //var localizationOptions = new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture(defaultCulture),
            //    SupportedCultures = new List<CultureInfo> { defaultCulture },
            //    SupportedUICultures = new List<CultureInfo> { defaultCulture }
            //};
            //app.UseRequestLocalization(localizationOptions);
            //Fin Culture

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

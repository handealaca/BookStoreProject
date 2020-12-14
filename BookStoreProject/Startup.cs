using BookStoreProject.Models.ORM.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<BookContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute
            //    ("Areas",
            //    "{ area: exists}/{ controller = Admin}/{ action = Index}/{ id ?}");
            //    routes.MapRoute
            //        (name: "default",
            //        template: "{Controller=AdminHome}/{Action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapAreaControllerRoute(
                     name: "Admin",
                     areaName: "Admin",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                     );
                    endpoints.MapControllerRoute("default", "{Controller=AdminHome}/{Action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });



        }
    }
}

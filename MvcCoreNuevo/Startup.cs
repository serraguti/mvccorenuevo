using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCoreNuevo.Data;
using MvcCoreNuevo.Repositories;

namespace MvcCoreNuevo
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            String sqlcnn =
                this.Configuration.GetConnectionString("cadenasqllocal");
            services.AddTransient<IRepositoryHospital
                , RepositoryHospital>();
            services.AddDbContext<HospitalContext>
                (options => options.UseSqlServer(sqlcnn));
            services.AddSession();
            services.AddControllersWithViews();
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
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}

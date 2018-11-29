using System.Net.Http.Formatting;
using EMS.CachingDbContext;
using EMS.ConfigurationDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EMS.UserManagement
{
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
            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<CachingContext>(options =>
                {
                    options.UseSqlite(Configuration.GetConnectionString("CachingDb"),
                        x => x.MigrationsAssembly("EMS.CachingDbContext"));
                })
                .AddDbContext<ConfigurationContext>(options =>
                {
                    options.UseSqlite(Configuration.GetConnectionString("ConfigurationDb"),
                        x => x.MigrationsAssembly("EMS.ConfigurationDbContext"));
                });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<CachingContext>().Database.MigrateAsync()
                    .GetAwaiter().GetResult();
                serviceScope.ServiceProvider.GetService<ConfigurationContext>().Database.MigrateAsync()
                    .GetAwaiter().GetResult();
            }

        }
    }
}
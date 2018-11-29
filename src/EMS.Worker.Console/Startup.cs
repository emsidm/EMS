using System;
using System.Collections.Generic;
using System.IO;
using EMS.CachingDbContext;
using EMS.ConfigurationProviders.WebApi;
using EMS.Contracts.DataAccess;
using EMS.Contracts.Workers;
using EMS.DataSources.EntityFramework;
using EMS.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Worker.Console
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataContext<CachingContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("CachingDb"));
            });

            services
                .AddOptions()
                .AddTransient<IWorker, Worker>()
                .BuildServiceProvider();
        }
    }
}
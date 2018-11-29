using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EMS.CachingDbContext;
using EMS.ConfigurationProviders.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Worker.Console
{
    class Program
    {
        private static IConfigurationRoot CreateConfiguration(string[] args)
        {

            var environmentConfiguration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            var localConfigurationBuilder = new ConfigurationBuilder();
            localConfigurationBuilder.AddConfiguration(environmentConfiguration);
            localConfigurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            localConfigurationBuilder.AddJsonFile("appsettings.json", optional: true);
            var localConfiguration = localConfigurationBuilder.Build();

            return new ConfigurationBuilder()
                .AddConfiguration(localConfiguration)
                .AddWebApi(localConfiguration.GetSection("API").Get<WebApiProviderOptions>())
                .Build();
        }

        private static IServiceProvider ConfigureServices(Startup startup)
        {
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            Startup startup = new Startup(CreateConfiguration(args));

            var serviceProvider = ConfigureServices(startup);

            System.Console.WriteLine(startup.Configuration["WorkerName"]);

            var context = serviceProvider.GetService<CachingContext>();

            System.Console.WriteLine(context.Users.ToList());

            System.Console.ReadLine();
        }
    }
}
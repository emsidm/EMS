using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Synchronizer.Console
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();

            startup.ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var workerService = serviceProvider.GetService<object>();

            System.Console.ReadLine();
        }
    }
}
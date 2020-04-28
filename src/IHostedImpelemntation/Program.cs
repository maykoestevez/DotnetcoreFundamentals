using System;
using IHostedImplementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IHostedImpelemntation
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder()
             .ConfigureServices((context, services) =>
             {
                 services.AddHostedService<ServiceWorker>();
             });
    }
}

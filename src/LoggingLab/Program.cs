using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoggingLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Loggin in main method
            // It has to be done after the host is build so we will have available all logger configuration needed

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Loggin from main method");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            // By Default this method add default providers:
            /*
             * Console
             * Debug
             * EventSource
             * EventLog
             */
            Host.CreateDefaultBuilder(args)
                // 
                .ConfigureLogging(logging =>
                {
                    // Configure filters
                    logging.AddFilter((provider, category, logLevel) =>
                    {
                        if (provider.Contains("ConsoleLoggerProvider")
                        && category.Contains("LoggingLab.Startup")
                        && logLevel >= LogLevel.Information)
                        {
                            return true;
                        }

                        return false;

                    });

                    logging.SetMinimumLevel(LogLevel.Information);// setting minum log level
                    // Cleanining providers
                    //logging.ClearProviders();

                    // Adding specific provider
                    //logging.AddConsole();
                    //logging.AddDebug();


                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

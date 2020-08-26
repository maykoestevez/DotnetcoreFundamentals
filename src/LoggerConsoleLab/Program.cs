using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LoggerConsoleLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //

            // configure logs
            // To create Logger Factory you need the Microsoft extensions.Logging
            // And also you havve to install the provoders that belogs to the same library but ends with the name of the provider
            var loggerFactory = LoggerFactory.Create(builder =>
            {

                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("LoggerConsoleLab.Program", LogLevel.Information)
                       .AddConsole();



            });

            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("testing logs");

            Console.WriteLine("Hello World!");

            // Cofigure host to create logger while cerating the host

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) {

            var builtConfig = new ConfigurationBuilder()
                                  .AddJsonFile("")
                                  .AddCommandLine(args)
                                  .Build();

             Log.Logger= new LoggerConfiguration()
                         .WriteTo.Console()
                         .WriteTo.File(builtConfig["Logging:FilePath"])
                         .CreateLogger();

            try
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((context, services) =>
                    {
                        //services.AddRazorPages();
                    })
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddConfiguration(builtConfig);
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.AddSerilog();
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        //webBuilder.UseStartup<Startup>();
                    });
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host builder error");

                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}

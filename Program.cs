using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace FundamentalsLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Acces to the services from main
            using (var scopeServices = host.Services.CreateScope())
            {
                var services = scopeServices.ServiceProvider;
                try
                {
                    var TestService = services.GetRequiredService<TestService>();
                    string myName = TestService.GetNameAsync().GetAwaiter().GetResult();
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // Add configure methods without startup class
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) => { })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddScoped<TestService>();
                        services.AddTransient<IStartupFilter, GlobalExeptionStartupFilter>();
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/test", async context =>
                            {
                                throw new InvalidOperationException("Test IAplicationFilter");
                                await context.Response.WriteAsync("Hello World!");
                            });
                        });
                    });
                });

        //Default startup configuration
        // Host.CreateDefaultBuilder(args)
        //     .ConfigureWebHostDefaults(webBuilder =>
        //     {
        //         webBuilder.UseStartup<Startup>();
        //     });
    }
}

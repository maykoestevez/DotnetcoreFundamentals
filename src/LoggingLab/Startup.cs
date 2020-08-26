using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoggingLab
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> log)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    //
                    log.LogDebug("Mayko");

                    log.LogInformation("Testing logger");

                    // log with ID
                    log.LogInformation(eventId: 1010,// specify event id to identify your log or group them.
                                      "Testing logger");
                    System.Diagnostics.Debug.WriteLine("Mayko Debug");
                    // log with message template
                    var id = 1;
                    var error = "error";
                    log.LogInformation($"Teting {error} with args {id}", 1, "error");

                    using (log.BeginScope("Mayko Scope"))
                    {
                        log.LogInformation("Log Exeption in scope");
                    }

                    try
                    {
                       
                        throw new InvalidOperationException("logging error");
                    }
                    catch (Exception ex)
                    {
                        log.LogError("Loging Error", ex, ex.Message);
                    }
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}

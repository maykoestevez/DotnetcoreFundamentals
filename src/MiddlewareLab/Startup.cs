using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddlewareLab
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Adding branch to match path
            app.Map("/test", (app) => { });

            //Adding branch to match a whole segment
            app.Map("/level/seg", (app) => { });

            /*the matched path segments are removed from HttpRequest.Path 
             and appended to HttpRequest.PathBase for each request.*/
            app.Map("/level", level1 =>
            {
                level1.Map("level2", level2 => { });
            });
            
            // Using Map with a predicate
            app.MapWhen(x => x.Request.Path.Equals("/level"), level => { });

             // Any of the others join the pipe line but with useWhen you can do it.
            app.UseWhen(x => x.Request.Path.Equals("/level"), level => { });

            //adding some work before response
            app.Use((context, next) =>
            {
                //do some work 

                //call next middleware
                return next.Invoke();
            });

            // Finishing pipe line with a short circuit call
            app.Run(async context =>
            {
                await context.Response.WriteAsync("My first middleware");
            });
        }
    }
}

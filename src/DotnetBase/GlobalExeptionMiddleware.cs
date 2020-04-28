using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace FundamentalsLab
{
    public class GlobalExeptionMiddleware
    {
        // 1. define request delegate
        private readonly RequestDelegate _next;

        // 2. inject request delegate
        public GlobalExeptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // 2. middleware process
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(context, ex);
            }
        }

        private Task HandleExeptionAsync(HttpContext context, Exception ex)
        {
            // Log work
            var errorString = JsonSerializer.Serialize(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(errorString);
        }
    }

    public class GlobalExeptionStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<GlobalExeptionMiddleware>();
                next(builder);
            };
        }
    }

}
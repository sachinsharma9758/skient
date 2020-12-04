using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context){
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                var response=env.IsDevelopment()
                ? new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                : new ApiException((int)HttpStatusCode.InternalServerError,ex.Message);
                var jsonOptions=new JsonSerializerOptions{
                    PropertyNamingPolicy=JsonNamingPolicy.CamelCase
                };
                var json=JsonSerializer.Serialize(response,jsonOptions);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
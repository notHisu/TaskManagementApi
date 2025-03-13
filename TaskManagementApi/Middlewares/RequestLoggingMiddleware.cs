using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;

namespace TaskManagementApi.Middlewares
{
    public class RequestLoggingMiddleware 
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;
            var timestamp = DateTime.Now;

            Console.WriteLine($"[{timestamp}] {method} request made to {path}");

            await _next(context);
        }
    }
}

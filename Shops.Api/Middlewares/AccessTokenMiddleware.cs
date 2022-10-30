using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Shops.Api.Middlewares
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _accessTokenPattern;

        public AccessTokenMiddleware(RequestDelegate next, string accessTokenPattern)
        {
            _next = next;
            _accessTokenPattern = accessTokenPattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/health-check")
            {
                await _next.Invoke(context);
                return;
            }

            StringValues token;
            try
            {
                if (!context.Request.Headers.TryGetValue("AccessToken", out token))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Token is invalid");
                    throw new Exception("Token is invalid");
                }

                if (token != _accessTokenPattern)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Token is invalid");
                    throw new Exception("Token is invalid");
                }
                await _next.Invoke(context);
            }
            catch
            {

            }
        }
    }
}
using Microsoft.Extensions.Configuration;

namespace Graduation.Hackaton.VideoProcessing.API.DependencyInjection.Middlewares
{
    public sealed class ApiKeyMiddleware
    {
        private readonly string _apiKey;
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration["ApiKey"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.ToString().ToLower().Contains("swagger") &&
                context.Request.Headers["X-API-KEY"] != _apiKey)
            {
                throw new UnauthorizedAccessException("Invalid api key");
            }

            await _next(context);
        }
    }
}
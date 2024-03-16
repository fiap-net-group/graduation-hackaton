using System.Net;
using System.Text.Json;
using Graduation.Hackaton.VideoProcessing.Domain.Exceptions;
using Graduation.Hackaton.VideoProcessing.Domain.Responses;
using MassTransit;

namespace Graduation.Hackaton.VideoProcessing.API.DependencyInjection.Middlewares
{
    public sealed class ErrorHandlingMiddleware
    {
       private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (exception is KeyNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is ArgumentException) code = HttpStatusCode.BadRequest;
            else if (exception is BusinessException businessException)
            {
                code = HttpStatusCode.BadRequest;
                var result = JsonSerializer.Serialize(new ErrorResponse(Enum.Parse<ResponseMessage>(businessException.Message), businessException.Errors));
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);                
            }

            var generalResult = JsonSerializer.Serialize(new ErrorResponse(Enum.Parse<ResponseMessage>(exception.Message)));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(generalResult);
        }
    }
}

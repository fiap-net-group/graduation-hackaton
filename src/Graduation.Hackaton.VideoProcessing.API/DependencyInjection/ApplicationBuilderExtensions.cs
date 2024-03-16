using Graduation.Hackaton.VideoProcessing.API.DependencyInjection.Middlewares;
using Graduation.Hackaton.VideoProcessing.API.DependencyInjection.Swagger;

namespace Graduation.Hackaton.VideoProcessing.API.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiDependencyInjection(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseSwaggerConfiguration();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMiddleware<ApiKeyMiddleware>();

            return app;
        }
    }
}

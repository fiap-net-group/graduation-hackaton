using Microsoft.AspNetCore.Http.Features;

namespace Graduation.Hackaton.VideoProcessing.MVC.DependencyInjection
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MultipartHeadersCountLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
                o.BufferBodyLengthLimit = int.MaxValue;
                o.BufferBody = true;
                o.ValueCountLimit = int.MaxValue;
            });

            return services;
        }
        internal static IApplicationBuilder UseMvcConfiguration(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=VideoProcessing}/{action=Send}/{id?}");

            return app;
        }
    }
}

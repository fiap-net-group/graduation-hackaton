namespace Graduation.Hackaton.VideoProcessing.MVC.DependencyInjection
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();

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

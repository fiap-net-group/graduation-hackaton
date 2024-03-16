namespace Graduation.Hackaton.VideoProcessing.MVC.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseDependencyInjection(this WebApplication app)
        {
            app.UseMvcConfiguration();

            return app;
        }
    }
}

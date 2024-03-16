using Graduation.Hackaton.VideoProcessing.MVC.Client;
using Graduation.Hackaton.VideoProcessing.MVC.Videos;

namespace Graduation.Hackaton.VideoProcessing.MVC.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcConfiguration()
                    .AddClientConfiguration()
                    .AddVideoConfiguration(configuration);

            return services;
        }
    }
}

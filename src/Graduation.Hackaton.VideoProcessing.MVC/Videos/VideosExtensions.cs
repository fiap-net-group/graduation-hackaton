using Graduation.Hackaton.VideoProcessing.MVC.Client;
using Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces;
using Graduation.Hackaton.VideoProcessing.MVC.Videos.UseCases;

namespace Graduation.Hackaton.VideoProcessing.MVC.Videos
{
    public static class VideosExtensions
    {
        public static IServiceCollection AddVideoConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(ClientExtensions.VideoClient, client =>
            {
                client.BaseAddress = new Uri(configuration["Api:Video:Endpoints:BaseAddress"]);
            });

            services.AddScoped<IVideoClient, VideoClient>();
            services.AddScoped<IListVideo, ListVideo>();
            services.AddScoped<IUploadVideo, UploadVideo>();


            return services;
        }
    }
}

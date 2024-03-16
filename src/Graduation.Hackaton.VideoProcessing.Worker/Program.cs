
using Graduation.Hackaton.VideoProcessing.Application;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions;
using Graduation.Hackaton.VideoProcessing.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Graduation.Hackaton.VideoProcessing.Worker
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json")
                .Build();


            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(context =>
                {
                    context.AddConfiguration(configuration);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddWorkerApplicationConfiguration();
                    services.AddLoggingGateway();
                    services.AddFileServer(configuration);
                    services.AddVideoProcessingApiIntegration(configuration);
                    services.AddWorkerEventGateway<ProcessVideoConsumer>(context.Configuration, typeof(ProcessVideoConsumer), nameof(ProcessVideoEvent));
                })
                .Build();

            host.Run();
        }
    }
}

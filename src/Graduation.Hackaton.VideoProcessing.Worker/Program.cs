
using Graduation.Hackaton.VideoProcessing.Application;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
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
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    context.Configuration = configurationBuilder.SetBasePath(context.HostingEnvironment.ContentRootPath)
                                                                .AddJsonFile("appsettings.json", true, true)
                                                                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                                                                .AddEnvironmentVariables()
                                                                .Build();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddWorkerApplicationConfiguration();
                    services.AddLoggingGateway();
                    services.AddWorkerEventGateway<ProcessVideoConsumer>(context.Configuration, typeof(ProcessVideoConsumer), nameof(ProcessVideoEvent));
                })
                .Build();

            host.Run();
        }
    }
}

using Microsoft.Extensions.Hosting;

namespace Graduation.Hackaton.VideoProcessing.Worker
{
    sealed class Worker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}

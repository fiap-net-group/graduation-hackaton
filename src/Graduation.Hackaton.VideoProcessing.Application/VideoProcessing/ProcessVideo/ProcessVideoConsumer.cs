using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions;
using Graduation.Hackaton.VideoProcessing.Domain.Extensions;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi.Boundaries;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo
{
    public sealed class ProcessVideoConsumer : IConsumer<ProcessVideoEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerGateway _logger;

        public ProcessVideoConsumer(IServiceProvider serviceProvider, ILoggerGateway logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProcessVideoEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();

            var processVideoUseCase = scope.ServiceProvider.GetRequiredService<IProcessVideoUseCase>();
            var videoProcessingApi = scope.ServiceProvider.GetRequiredService<IVideoProcessingApiGateway>();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var cancellationToken = configuration.GenerateCancellationToken();

            _logger.Log("Begin executing Video processing business rule", LoggerManagerSeverity.DEBUG, (LoggingConstants.Event, context.Message));
            var output = await processVideoUseCase.ProcessAsync(context.Message.Value, cancellationToken);

            _logger.Log("End executing Video processing business rule", LoggerManagerSeverity.DEBUG,
                    (LoggingConstants.Event, context.Message),
                    (LoggingConstants.Output, output));

            _logger.Log("Begin sending Video processing status to API", LoggerManagerSeverity.DEBUG);
            await videoProcessingApi.UpdateAsync(new UpdateVideoInput(), cancellationToken);
            _logger.Log("End sending Video processing status to API", LoggerManagerSeverity.DEBUG);
        }
    }
}

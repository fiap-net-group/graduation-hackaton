using AutoMapper;
using FluentValidation;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions;
using Graduation.Hackaton.VideoProcessing.Domain.Extensions;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.File;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.File.Boundaires;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;
using Graduation.Hackaton.VideoProcessing.Domain.ValueObject;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo
{
    public sealed class ProcessVideoInteractor : IProcessVideoUseCase
    {
        private readonly ILoggerGateway _logger;
        private readonly IFileGateway _fileGateway;
        private readonly IValidator<ProcessVideoInput> _validator;
        private readonly IMapper _mapper;

        public ProcessVideoInteractor(
            ILoggerGateway logger,
            IFileGateway fileGateway,
            IValidator<ProcessVideoInput> validator,
            IMapper mapper)
        {
            _logger = logger;
            _fileGateway = fileGateway;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ProcessVideoOutput> ProcessAsync(ProcessVideoInput input, CancellationToken cancellationToken)
        {
            var (isValid, output) = await _validator.ValidateInputAsync<ProcessVideoInput, ProcessVideoOutput>(input, _mapper, _logger, cancellationToken);

            if (!isValid) return output;

            _logger.Log("Starting processing video", LoggerManagerSeverity.INFORMATION, (LoggingConstants.Input, input));

            var videoInfo = await _fileGateway.GetVideoInfoAsync(_mapper.Map<GetVideoInfoInput>(input), cancellationToken);

            if (!videoInfo.Exists)
            {
                _logger.Log("Video don't exists", LoggerManagerSeverity.WARNING, (LoggingConstants.Input, input));

                input.Entity.Status = VideoProcessingStatus.VideoNotFound;

                return new ProcessVideoOutput(input.Entity);
            }

            _logger.Log("Video exists, starting process", LoggerManagerSeverity.DEBUG, (LoggingConstants.Input, input));

            await _fileGateway.CreateLocalFile(videoInfo, cancellationToken);

            input.Entity.ImagesPath = ImagesExtensions.GetImagePath(input.Entity.Name);

            var snapShotSaves = new List<Task>();

            for (var currentTime = TimeSpan.Zero; currentTime < videoInfo.Duration; currentTime += input.IntervalAsTimeSpan)
                snapShotSaves.Add(_fileGateway.SaveSnapshot(input.Entity.ImagesPath, videoInfo, currentTime, cancellationToken));

            await Task.WhenAll(snapShotSaves);

            _logger.Log("Video successfully processed", LoggerManagerSeverity.DEBUG, (LoggingConstants.Input, input));

            input.Entity.Status = VideoProcessingStatus.Processed;

            return new ProcessVideoOutput(input.Entity);
        }
    }
}

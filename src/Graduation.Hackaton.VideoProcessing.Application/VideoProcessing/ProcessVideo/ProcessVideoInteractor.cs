using FluentValidation;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions;
using Graduation.Hackaton.VideoProcessing.Domain.Extensions;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.File;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo
{
    public class ProcessVideoInteractor : IProcessVideoUseCase
    {
        private readonly ILoggerGateway _logger;
        private readonly IFileGateway _fileGateway;
        private readonly IValidator<ProcessVideoInput> _validator;

        public ProcessVideoInteractor(
            ILoggerGateway logger, 
            IFileGateway fileGateway, 
            IValidator<ProcessVideoInput> validator)
        {
            _logger = logger;
            _fileGateway = fileGateway;
            _validator = validator;
        }

        public async Task<ProcessVideoOutput> ProcessAsync(ProcessVideoInput input, CancellationToken cancellationToken)
        {
            await _validator.ThrowIfInvalidAsync(input, _logger, cancellationToken);

            return new ProcessVideoOutput();
        }
    }
}

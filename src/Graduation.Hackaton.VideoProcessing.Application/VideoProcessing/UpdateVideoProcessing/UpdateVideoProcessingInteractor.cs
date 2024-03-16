using FluentValidation;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Definitions;
using Graduation.Hackaton.VideoProcessing.Domain.Exceptions;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Repositories;
using Graduation.Hackaton.VideoProcessing.Domain.Responses;
using Microsoft.Extensions.Logging;
using System;
using Graduation.Hackaton.VideoProcessing.Domain.Extensions;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing
{
    public sealed class UpdateVideoProcessingInteractor : IUpdateVideoProcessingUseCase
    {
        private readonly ILoggerGateway _logger;
        private readonly IValidator<UpdateVideoProcessingInput> _validator;
        private readonly IVideoRepositoryGateway _repositoryGateway;
        public UpdateVideoProcessingInteractor(ILoggerGateway logger, IValidator<UpdateVideoProcessingInput> validator, IVideoRepositoryGateway repositoryGateway)
        {
            _logger = logger;
            _validator = validator;
            _repositoryGateway = repositoryGateway;
        }

        public async Task UpdateAsync(UpdateVideoProcessingInput input, CancellationToken cancellationToken)
        {
            _logger.Log("Starting updating the request status", LoggerManagerSeverity.INFORMATION, (LoggingConstants.Input, input));

            await _validator.ThrowIfInvalidAsync(input, _logger, cancellationToken);

            var video = await _repositoryGateway.GetByIdAsync(input.value.Id, cancellationToken);

            if (!video.Enabled)
                throw new BusinessException(ResponseMessage.NotEnabled.ToString());

            await _repositoryGateway.UpdateAsync(video, cancellationToken);

            _logger.Log("video updated", LoggerManagerSeverity.INFORMATION, (LoggingConstants.Input, input));


        }
    }
}

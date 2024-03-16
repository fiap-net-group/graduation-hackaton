using FluentValidation;
using Graduation.Hackaton.VideoProcessing.Domain.Exceptions;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;
using Graduation.Hackaton.VideoProcessing.Domain.Responses;

namespace Graduation.Hackaton.VideoProcessing.Domain.Extensions
{
    public static class ValidatorExtensions
    {
        public static async Task ThrowIfInvalidAsync<TInput>(this IValidator<TInput> validator, TInput input, ILoggerGateway logger, CancellationToken cancellationToken)
        {
            logger.Log("Validating the input", LoggerManagerSeverity.DEBUG, (LoggingConstants.Input, input));

            var result = await validator.ValidateAsync(input, cancellationToken);

            if (result.IsValid)
            {
                logger.Log("Input is valid", LoggerManagerSeverity.DEBUG, (LoggingConstants.Input, input));
                return;
            }

            throw new BusinessException(ResponseMessage.ValidationError.ToString(), result.Errors.Select(e => e.ErrorMessage).ToArray());
        }

    }
}

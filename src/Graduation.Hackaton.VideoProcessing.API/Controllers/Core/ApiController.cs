using Microsoft.AspNetCore.Mvc;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.SendVideoToProcess.Definitions;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Definitions;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.SendVideoToProcess.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Boundaries;

namespace Graduation.Hackaton.VideoProcessing.API.Controllers.Core;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ApiController : ControllerBase
{
    private readonly int _cancelRequisitionAfterInSeconds;
    private readonly IProcessVideoUseCase _processVideoUseCase;
    private readonly ISendVideoToProcessUseCase _sendVideoToProcessUseCase;
    private readonly IUpdateVideoProcessingUseCase _updateVideoProcessingUseCase;

    public ApiController(IConfiguration configuration, IProcessVideoUseCase processVideoUseCase, ISendVideoToProcessUseCase sendVideoToProcessUseCase, IUpdateVideoProcessingUseCase updateVideoProcessingUseCase)
    {
        _cancelRequisitionAfterInSeconds = configuration.GetValue<int>("CancelRequisitionAfterInSeconds", 30);
        _processVideoUseCase = processVideoUseCase;
        _sendVideoToProcessUseCase = sendVideoToProcessUseCase;
        _updateVideoProcessingUseCase = updateVideoProcessingUseCase;
    }

    protected CancellationToken AsCombinedCancellationToken(CancellationToken requestCancellationToken)
    {
        using var combinedCancellationTokens = CancellationTokenSource.CreateLinkedTokenSource(requestCancellationToken, HttpContext.RequestAborted);

        combinedCancellationTokens.CancelAfter(_cancelRequisitionAfterInSeconds);

        return combinedCancellationTokens.Token;
    }

    protected async Task<IActionResult> ProcessVideoAsync(ProcessVideoInput input, CancellationToken cancellationToken)
    {
        var output = await _processVideoUseCase.ProcessAsync(input, cancellationToken);

        return Ok(output);
    }

    protected async Task<IActionResult> SendVideoToProcessAsync(SendVideoToProcessInput input, CancellationToken cancellationToken)
    {
        // var output = await _sendVideoToProcessUseCase.SendVideoAsync(input, cancellationToken);

        // return Ok(output);
        return Ok();
    }

    protected async Task<IActionResult> UpdateVideoProcessingAsync(UpdateVideoProcessingInput input, CancellationToken cancellationToken)
    {
        // var output = await _updateVideoProcessingUseCase.UpdateAsync(input, cancellationToken);

        // return Ok(output);
        return Ok();
    }
}

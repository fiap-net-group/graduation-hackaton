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

    public ApiController(IConfiguration configuration)
    {
        _cancelRequisitionAfterInSeconds = configuration.GetValue<int>("CancelRequisitionAfterInSeconds", 30);
       
    }

    protected CancellationToken AsCombinedCancellationToken(CancellationToken requestCancellationToken)
    {
        using var combinedCancellationTokens = CancellationTokenSource.CreateLinkedTokenSource(requestCancellationToken, HttpContext.RequestAborted);

        combinedCancellationTokens.CancelAfter(_cancelRequisitionAfterInSeconds);

        return combinedCancellationTokens.Token;
    }
}

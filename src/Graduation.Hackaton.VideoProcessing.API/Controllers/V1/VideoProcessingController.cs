using Graduation.Hackaton.VideoProcessing.API.Controllers.Core;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.SendVideoToProcess.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.SendVideoToProcess.Definitions;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Hackaton.VideoProcessing.API.Controllers.V1
{
    public class VideoProcessingController : ApiController
    {
        private readonly ISendVideoToProcessUseCase _sendVideoToProcessUseCase;
        private readonly IUpdateVideoProcessingUseCase _updateVideoProcessingUseCase;

        public VideoProcessingController(IConfiguration configuration, IProcessVideoUseCase processVideoUseCase, ISendVideoToProcessUseCase sendVideoToProcessUseCase, IUpdateVideoProcessingUseCase updateVideoProcessingUseCase) : base(configuration, processVideoUseCase, sendVideoToProcessUseCase, updateVideoProcessingUseCase)
        {
            _sendVideoToProcessUseCase = sendVideoToProcessUseCase;
            _updateVideoProcessingUseCase = updateVideoProcessingUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file, CancellationToken cancellationToken)
        {
            await _sendVideoToProcessUseCase.Se

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateVideoProcessingInput input, CancellationToken cancellationToken)
        {
            await _updateVideoProcessingUseCase.UpdateAsync(input, cancellationToken);

            return NoContent();
        }
    }
}

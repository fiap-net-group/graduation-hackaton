using Graduation.Hackaton.VideoProcessing.MVC.Models;
using Graduation.Hackaton.VideoProcessing.MVC.Models.Responses;
using Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces;

namespace Graduation.Hackaton.VideoProcessing.MVC.Videos.UseCases
{
    public class UploadVideo : IUploadVideo
    {
        private readonly IVideoClient _client;

        public UploadVideo(IVideoClient client)
        {
            _client = client;
        }
        public async Task<BaseResponse> RunAsync(UploadVideoProcessingViewModel model, CancellationToken cancellationToken)
        {
            var videoResponse = await _client.UploadVideo(model.VideoFile, model.Description, cancellationToken);

            return videoResponse;
        }
    }
}

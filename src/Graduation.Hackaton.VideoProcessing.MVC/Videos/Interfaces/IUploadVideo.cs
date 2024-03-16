using Graduation.Hackaton.VideoProcessing.MVC.Models;
using Graduation.Hackaton.VideoProcessing.MVC.Models.Responses;

namespace Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces
{
    public interface IUploadVideo
    {
        Task<BaseResponse> RunAsync(UploadVideoProcessingViewModel viewModel, CancellationToken cancellationToken);
    }
}

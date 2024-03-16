using Graduation.Hackaton.VideoProcessing.MVC.Models;
using Graduation.Hackaton.VideoProcessing.MVC.Models.Responses;

namespace Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces
{
    public interface IVideoClient
    {
        Task<BaseResponse> UploadVideo(IFormFile video, string description,CancellationToken token);
        Task<BaseResponseWithValue<IEnumerable<VideoDetails>>> ListVideo(CancellationToken token);
    }
}

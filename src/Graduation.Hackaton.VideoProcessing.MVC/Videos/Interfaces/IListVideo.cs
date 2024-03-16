using Graduation.Hackaton.VideoProcessing.MVC.Models;
using Graduation.Hackaton.VideoProcessing.MVC.Models.Responses;

namespace Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces
{
    public interface IListVideo
    {
        Task<BaseResponseWithValue<IEnumerable<VideoDetails>>> RunAsync(CancellationToken cancellationToken);
    }
}

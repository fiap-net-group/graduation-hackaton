using Graduation.Hackaton.VideoProcessing.MVC.Models;
using Graduation.Hackaton.VideoProcessing.MVC.Models.Responses;
using Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces;

namespace Graduation.Hackaton.VideoProcessing.MVC.Videos.UseCases
{
    public class ListVideo : IListVideo
    {
        private readonly IVideoClient _client;

        public ListVideo(IVideoClient client)
        {
            _client = client;
        }
        public async Task<BaseResponseWithValue<IEnumerable<VideoDetails>>> RunAsync(CancellationToken cancellationToken)
        {
            var response = await _client.ListVideo(cancellationToken);

            return response;
        }
    }
}

using Graduation.Hackaton.VideoProcessing.MVC.Models;
using Graduation.Hackaton.VideoProcessing.MVC.Models.Responses;
using Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces;
using Graduation.Hackaton.VideoProcessing.MVC.Client;
using System.Text.Json;
using Polly.Retry;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Graduation.Hackaton.VideoProcessing.MVC.Models.Request;

namespace Graduation.Hackaton.VideoProcessing.MVC.Videos
{
    public class VideoClient : BaseClient, IVideoClient
    {
        private readonly JsonSerializerOptions _serializeOptions;

        private readonly string _uploadVideoUrl;
        private readonly string _listVideoUrl;

        public VideoClient(AsyncRetryPolicy<HttpResponseMessage> retryPolicy, 
                            JsonSerializerOptions serializeOptions, 
                            IHttpClientFactory clientFactory, 
                            IConfiguration configuration) : base(retryPolicy, serializeOptions, clientFactory.CreateClient(ClientExtensions.VideoClient))
        {
            _serializeOptions = serializeOptions;
            _uploadVideoUrl = configuration["Api:Video:Endpoints:UploadVideo"];
            _listVideoUrl = configuration["Api:Video:Endpoints:ListVideo"];
        }

        public async Task<BaseResponseWithValue<IEnumerable<VideoDetails>>> ListVideo(CancellationToken token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _listVideoUrl);

            try
            {
                return await SendAsync<BaseResponseWithValue<IEnumerable<VideoDetails>>>(request, token);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<BaseResponse> UploadVideo(IFormFile video, string description, CancellationToken token)
        {
            await using var videoStram = video.OpenReadStream();
            using var bynaryReader = new BinaryReader(videoStram);
            var data = bynaryReader.ReadBytes((int)videoStram.Length);

            var videoContent = JsonSerializer.Serialize(new UploadVideoViewModel
            {
                Name = video.FileName,
                VideoUrl = Path.GetExtension(video.FileName),
                VideoByte = data
            },
            _serializeOptions);

            var request = new HttpRequestMessage(HttpMethod.Post, _uploadVideoUrl)
            {
                Content = new StringContent(videoContent, Encoding.UTF8, "application/json")
            };

            try
            {
                return await SendAsync<BaseResponse>(request, token);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

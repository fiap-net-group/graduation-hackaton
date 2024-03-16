using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi.Boundaries;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Graduation.Hackaton.VideoProcessing.Infrastructure.Integrations
{
    public class VideoProcessingApiGateway : IVideoProcessingApiGateway
    {
        private readonly HttpClient _httpClient;
        private readonly string _updateEndpoint;

        public VideoProcessingApiGateway(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _updateEndpoint = configuration.GetValue("Gateways:Integrations:VideoProcessingApi:Endpoints:V1:UpdateVideoProcessing", "");
        }

        public async Task UpdateAsync(UpdateVideoInput input, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(_updateEndpoint, input, cancellationToken);

                if(!response.IsSuccessStatusCode)
                {
                    //log
                }

                return;
            }
            catch (Exception ex)
            {
                //log

                return;
            }
        }
    }
}

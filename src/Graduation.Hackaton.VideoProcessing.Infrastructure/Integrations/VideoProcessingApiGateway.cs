using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi.Boundaries;

namespace Graduation.Hackaton.VideoProcessing.Infrastructure.Integrations
{
    public class VideoProcessingApiGateway : IVideoProcessingApiGateway
    {
        public Task UpdateAsync(UpdateVideoInput input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

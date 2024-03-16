using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi.Boundaries;

namespace Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi
{
    public interface IVideoProcessingApiGateway
    {
        Task UpdateAsync(UpdateVideoInput input, CancellationToken cancellationToken);
    }
}

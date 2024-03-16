using Graduation.Hackaton.VideoProcessing.Domain.Gateways.File.Boundaires;

namespace Graduation.Hackaton.VideoProcessing.Domain.Gateways.File
{
    public interface IFileGateway
    {
        Task<GetVideoInfoOutput> GetVideoInfoAsync(GetVideoInfoInput input, CancellationToken cancellationToken);
        Task CreateLocalFile(GetVideoInfoOutput videoInfo, CancellationToken cancellationToken);
        Task SaveSnapshot(string fileName, GetVideoInfoOutput videoInfo, TimeSpan currentTime, CancellationToken cancellationToken);
    }
}

using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions
{
    public interface IProcessVideoUseCase
    {
        Task<ProcessVideoOutput> ProcessAsync(ProcessVideoInput input, CancellationToken cancellationToken);
    }
}

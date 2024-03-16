using Graduation.Hackaton.VideoProcessing.Domain.Entities;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries
{
    public sealed class ProcessVideoInput
    {
        public VideoEntity Entity { get; set; }
        public int Interval { get; set; }
    }
}

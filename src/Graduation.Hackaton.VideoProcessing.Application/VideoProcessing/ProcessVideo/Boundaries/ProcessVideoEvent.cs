using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Event;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries
{
    public sealed class ProcessVideoEvent : BaseEvent<ProcessVideoInput>
    {
        public override string OperationName => nameof(ProcessVideoEvent);
    }
}

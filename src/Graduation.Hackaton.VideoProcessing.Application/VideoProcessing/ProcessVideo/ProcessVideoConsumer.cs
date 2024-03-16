using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using MassTransit;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo
{
    public class ProcessVideoConsumer : IConsumer<ProcessVideoEvent>
    {
        public Task Consume(ConsumeContext<ProcessVideoEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}

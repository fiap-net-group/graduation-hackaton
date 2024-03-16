using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries
{
    public class ProcessVideoEvent : BaseEvent<ProcessVideoInput>
    {
        public override string OperationName => nameof(ProcessVideoEvent);
    }
}

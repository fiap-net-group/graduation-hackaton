using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Boundaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Definitions
{
    public interface IUpdateVideoProcessingUseCase
    {
        Task UpdateAsync(UpdateVideoProcessingInput input, CancellationToken cancellationToken);
    }
}

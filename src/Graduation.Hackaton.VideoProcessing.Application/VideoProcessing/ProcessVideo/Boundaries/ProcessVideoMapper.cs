using AutoMapper;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.File.Boundaires;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries
{
    public sealed class ProcessVideoMapper : Profile
    {
        public ProcessVideoMapper()
        {
            CreateMap<ProcessVideoInput, GetVideoInfoInput>()
                .ConstructUsing(source => new GetVideoInfoInput(source.Entity.VideoPath));
        }
    }
}

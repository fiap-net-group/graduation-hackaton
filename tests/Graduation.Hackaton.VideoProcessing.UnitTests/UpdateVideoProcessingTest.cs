using Moq;
using AutoMapper;
using Xunit.Abstractions;
using Graduation.Hackaton.VideoProcessing.Domain.Entities;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Definitions;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Boundaries;
using Graduation.Hackaton.VideoProcessing.Domain.ValueObject;

namespace Graduation.Hackaton.VideoProcessing.UnitTests
{
    public class UpdateVideoProcessingTest
    {
        Mock<IUpdateVideoProcessingUseCase> _updateVideoUseCaseMock;
        public ITestOutputHelper SaidaConsoleTeste;
        public IMapper _mapper;

        [Fact(DisplayName = "Alterar video")]
        public async void AlterarVideo()
        {
            //Arrange
            VideoEntity video = new VideoEntity();

            video.Status = VideoProcessingStatus.Processed;
            UpdateVideoProcessingInput videoInput = new UpdateVideoProcessingInput(video);
            //_updateVideoUseCaseMock.Setup(x => x.UpdateAsync(videoInput, new CancellationToken()));

            //Act

            //Assert
            Assert.Equal("Processed", video.Status.ToString());
        }
    }
}
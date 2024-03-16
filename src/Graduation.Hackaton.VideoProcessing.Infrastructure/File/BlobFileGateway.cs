using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FFMpegCore;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.File;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.File.Boundaires;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;
using System.Drawing;

namespace Graduation.Hackaton.VideoProcessing.Infrastructure.File
{
    public sealed class BlobFileGateway : IFileGateway
    {
        private readonly ILoggerGateway _logger;
        private readonly BlobContainerClient _client;

        public BlobFileGateway(ILoggerGateway logger, BlobContainerClient blobClient)
        {
            _logger = logger;
            _client = blobClient;
        }

        public async Task<GetVideoInfoOutput> GetVideoInfoAsync(GetVideoInfoInput input, CancellationToken cancellationToken)
        {
            var file = _client.GetBlobClient(input.VideoPath);

            if (!await file.ExistsAsync(cancellationToken))
            {
                _logger.Log("Video was not found", LoggerManagerSeverity.WARNING,
                    (LoggingConstants.Validation, input.VideoPath));

                return new GetVideoInfoOutput(false, string.Empty, TimeSpan.Zero, null);
            }

            _logger.Log("Video was found", LoggerManagerSeverity.DEBUG,
                    (LoggingConstants.Validation, input.VideoPath));

            using var fileReadingStream = await file.OpenReadAsync(cancellationToken: cancellationToken);

            var videoInfo = FFProbe.Analyse(fileReadingStream);

            return new GetVideoInfoOutput(true, input.VideoPath, videoInfo.Duration, fileReadingStream);
        }

        public async Task CreateLocalFile(GetVideoInfoOutput videoInfo, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                var videoPath = GetVideoPath(videoInfo);
                
                using var videoStream = System.IO.File.Open(GetVideoPath(videoInfo), FileMode.Create);

                videoInfo.File.CopyTo(videoStream);

                videoStream.Flush();
            },
            cancellationToken);
        }

        public async Task SaveSnapshot(string fileName, GetVideoInfoOutput videoInfo, TimeSpan currentTime, CancellationToken cancellationToken)
        {
            var imageName = $"{fileName}_{currentTime.TotalSeconds}.jpg";

            var imagePath = Path.Combine(Path.GetTempPath(), imageName);

            FFMpeg.Snapshot(GetVideoPath(videoInfo), imagePath, new Size(1920, 1080), currentTime);

            using var imageStream = new FileStream(imagePath, FileMode.Open);

            var blobFile = _client.GetBlobClient($"{fileName}/{imageName}");

            await blobFile.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = "image/jpeg" }, cancellationToken: cancellationToken);
        }

        private static string GetVideoPath(GetVideoInfoOutput videoInfo) => Path.Combine(Path.GetTempPath(), videoInfo.FileName);
    }
}

namespace Graduation.Hackaton.VideoProcessing.Domain.Gateways.File.Boundaires
{
    public record GetVideoInfoOutput(bool Exists, string FileName, TimeSpan Duration, Stream File);
}
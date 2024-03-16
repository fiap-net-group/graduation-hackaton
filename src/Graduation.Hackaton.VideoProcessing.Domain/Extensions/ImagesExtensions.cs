namespace Graduation.Hackaton.VideoProcessing.Domain.Extensions;

public class ImagesExtensions
{
    public static string GetImagePath(string videoName) => videoName.Replace(" ", "_");
}

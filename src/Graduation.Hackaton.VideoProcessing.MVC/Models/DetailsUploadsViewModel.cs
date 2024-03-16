namespace Graduation.Hackaton.VideoProcessing.MVC.Models
{
    public class DetailsUploadsViewModel
    {
        public IEnumerable<VideoDetails> Videos { get; set; }
    }

    public class VideoDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime CreateAt { get; set; }
        public int Status { get; set; }
    }
}

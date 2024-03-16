namespace Graduation.Hackaton.VideoProcessing.MVC.Models.Request
{
    public class UploadVideoViewModel
    {
        public string Name { get; set; }
        public string VideoUrl { get; set; }

        public byte[] VideoByte { get; set; }
    }
}
}

using System.ComponentModel.DataAnnotations;

namespace Graduation.Hackaton.VideoProcessing.MVC.Models
{
    public class UploadVideoProcessingViewModel
    {
        public string? Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile VideoFile { get; set; }
    }
}

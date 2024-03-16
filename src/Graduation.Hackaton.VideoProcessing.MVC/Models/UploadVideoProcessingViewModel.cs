using System.ComponentModel.DataAnnotations;

namespace Graduation.Hackaton.VideoProcessing.MVC.Models
{
    public class UploadVideoProcessingViewModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile VideoFile { get; set; }
}

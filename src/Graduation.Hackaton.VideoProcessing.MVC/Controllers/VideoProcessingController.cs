using Graduation.Hackaton.VideoProcessing.MVC.Models;
using Graduation.Hackaton.VideoProcessing.MVC.Videos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Graduation.Hackaton.VideoProcessing.MVC.Controllers
{
    public class VideoProcessingController : BaseController
    {
        private readonly ILogger<VideoProcessingController> _logger;
        private readonly IListVideo _listVideo;
        private readonly IUploadVideo _uploadVideo;

        public VideoProcessingController(ILogger<VideoProcessingController> logger, IUploadVideo uploadVideo,IListVideo listVideo)
        {
            _logger = logger;
            _listVideo = listVideo;
            _uploadVideo = uploadVideo;
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveVideo(UploadVideoProcessingViewModel uploadVideoProcessingViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug("Invalid SaveVideo");

                return View(nameof(Send), uploadVideoProcessingViewModel);
            }

            await _uploadVideo.RunAsync(uploadVideoProcessingViewModel, CancellationToken.None);

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> List()
        {
            var list = await _listVideo.RunAsync(CancellationToken.None);

            var model = new DetailsUploadsViewModel
            {
                Videos = list.Value
            };
            
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.ComponentModel;

namespace Graduation.Hackaton.VideoProcessing.MVC.Models.Responses
{
    public enum ResponseMessage
    {
        [Description("An error ocurred, try again later")]
        GenericError = 0,
        [Description("The video was uploaded successfully")]
        Success = 1,

    }
}

using Graduation.Hackaton.VideoProcessing.Domain.Entities;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries
{
    public sealed class ProcessVideoInput
    {
        private int _intervalInSeconds;
        private TimeSpan _intevalAsTimeSpan;

        public VideoEntity Entity { get; set; }
        public int IntervalInSeconds
        {
            get => _intervalInSeconds;
            set
            {
                if (value <= 0)
                {
                    _intervalInSeconds = 0;
                    _intevalAsTimeSpan = TimeSpan.FromSeconds(value);
                    return;
                }
                _intervalInSeconds = value;
                _intevalAsTimeSpan = TimeSpan.FromSeconds(value);
            }
        }

        public TimeSpan IntervalAsTimeSpan => _intevalAsTimeSpan;
    }
}

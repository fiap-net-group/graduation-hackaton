using MongoDB.Bson;

namespace Graduation.Hackaton.VideoProcessing.Domain.Entities
{
    public class VideoEntity
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

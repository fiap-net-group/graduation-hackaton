using MongoDB.Driver;

namespace Graduation.Hackaton.VideoProcessing.Infrastructure.Database.Configuration
{
    public interface IMongoDbGateway
    {
        IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName);
    }
}

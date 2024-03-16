using Graduation.Hackaton.VideoProcessing.Domain.Entities;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Repositories;
using Graduation.Hackaton.VideoProcessing.Infrastructure.Database.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.Hackaton.VideoProcessing.Infrastructure.Database.Repositories
{
    public class MongoDbVideoRepositoryGateway : IVideoRepositoryGateway
    {
        private readonly IMongoDbGateway _mongoDbGateway;

        public MongoDbVideoRepositoryGateway(IMongoDbGateway mongoDbGateway)
        {
            _mongoDbGateway = mongoDbGateway;
        }

        private IMongoCollection<VideoEntity> Collection =>
            _mongoDbGateway.GetCollection<VideoEntity>(nameof(VideoEntity));

        public async Task<VideoEntity> GetByIdAsync(ObjectId id, CancellationToken cancellationToken)
        {
            var entity = await (await Collection.FindAsync(filter => filter.Id == id, cancellationToken: cancellationToken)).SingleOrDefaultAsync(cancellationToken);

            return entity == null ?
                new VideoEntity
                {
                    Enabled = false
                } :
                entity.AsEntity();
        }

        public async Task<VideoEntity> UpdateAsync(VideoEntity video, CancellationToken cancellationToken)
        {

            var update = Builders<VideoEntity>.Update.Set(entity => entity.UpdatedAt, video.UpdatedAt)
                                                                  .Set(entity => entity.ImagesPath, video.ImagesPath)
                                                                  .Set(entity => entity.Status, video.Status);

            await Collection.FindOneAndUpdateAsync(filter => filter.Id == video.Id ,update, cancellationToken: cancellationToken);

            return video;
        }

    }
}

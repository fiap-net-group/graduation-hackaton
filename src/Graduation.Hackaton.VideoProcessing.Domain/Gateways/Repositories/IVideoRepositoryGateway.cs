using Graduation.Hackaton.VideoProcessing.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.Hackaton.VideoProcessing.Domain.Gateways.Repositories
{
    public interface IVideoRepositoryGateway
    {
        Task<VideoEntity> UpdateAsync(VideoEntity video, CancellationToken cancellationToken);

        Task<VideoEntity> GetByIdAsync(ObjectId id, CancellationToken cancellationToken);
    }
}

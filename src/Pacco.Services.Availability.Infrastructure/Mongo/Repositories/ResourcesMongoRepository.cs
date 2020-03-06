using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Repositories
{
    internal sealed class ResourcesMongoRepository : IResourcesRepository
    {
        private IMongoRepository<ResourceDocument, Guid> _repository;

        public ResourcesMongoRepository(IMongoRepository<ResourceDocument, Guid> mongoRepository)
        {
            _repository = mongoRepository;
        }
        public Task AddAsync(Resource resource) => _repository.AddAsync(resource.AsDocument());

        public Task DeleteAsync(AggregateId id) => _repository.DeleteAsync(id);

        public async Task<bool> ExistsAsync(AggregateId id) => await _repository.ExistsAsync(r => r.Id == id);

        public async Task<Resource> GetAsync(AggregateId id)
        {
            var document = await _repository.GetAsync(r => r.Id == id);

            return document?.AsEntity();
        }

        public Task UpdateAsync(Resource resource) => _repository.Collection.ReplaceOneAsync(r => r.Id == resource.Id && r.Version < resource.Version,
            resource.AsDocument());
    }
}

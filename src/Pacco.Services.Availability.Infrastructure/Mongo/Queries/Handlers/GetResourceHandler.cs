using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Application.Queries;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Queries.Handlers
{
    internal sealed class GetResourceHandler : IQueryHandler<GetResource, ResourceDto>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetResourceHandler(IMongoDatabase mongoDatabase)
        {
            this._mongoDatabase = mongoDatabase;
        }
        public async Task<ResourceDto> HandleAsync(GetResource query)
        {
            var document = await _mongoDatabase.GetCollection<ResourceDocument>("resources").Find(r => r.Id == query.ResourceId).SingleOrDefaultAsync();

            return document?.AsDto() ;
        }
    }
}

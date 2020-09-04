using Pacco.Services.Availability.Core.Entities;

namespace Pacco.Services.Availability.Core.Events
{
    public class ResourceDeleted : IDomainEvent
    {
        private AggregateId id;

        public Resource Resource { get; }

        public ResourceDeleted(Resource resource)
            => Resource = resource;

        public ResourceDeleted(AggregateId id)
        {
            this.id = id;
        }
    }
}
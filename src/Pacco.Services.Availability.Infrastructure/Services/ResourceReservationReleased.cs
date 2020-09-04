using Convey.CQRS.Events;
using Pacco.Services.Availability.Core.Entities;
using System;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal class ResourceReservationReleased : IEvent
    {
        private AggregateId id;
        private DateTime dateTime;

        public ResourceReservationReleased(AggregateId id, DateTime dateTime)
        {
            this.id = id;
            this.dateTime = dateTime;
        }
    }
}
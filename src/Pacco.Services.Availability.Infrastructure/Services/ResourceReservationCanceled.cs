using Convey.CQRS.Events;
using Pacco.Services.Availability.Core.Entities;
using System;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal class ResourceReservationCanceled : IEvent
    {
        private AggregateId id;
        private DateTime dateTime;

        public ResourceReservationCanceled(AggregateId id, DateTime dateTime)
        {
            this.id = id;
            this.dateTime = dateTime;
        }
    }
}
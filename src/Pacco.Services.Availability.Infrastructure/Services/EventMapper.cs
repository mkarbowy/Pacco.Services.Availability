using Convey.CQRS.Events;
using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal sealed class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
            => @event switch
            {
                ResourceCreated e => (IEvent)new ResourceAdded(e.Resource.Id),
                //ResourceDeleted e => new ResourceDeleted(e.Resource),
                ReservationAdded e => new ResourceReserved(e.Resource.Id, e.Reservation.DateTime),
                    ReservationReleased e => new ResourceReservationReleased(e.Resource.Id, e.Reservation.DateTime),
                    ReservationCanceled e => new ResourceReservationCanceled(e.Resource.Id, e.Reservation.DateTime),
                    _ => null
            };
    }
}

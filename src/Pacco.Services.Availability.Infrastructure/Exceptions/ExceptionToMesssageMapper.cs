using Convey.MessageBrokers.RabbitMQ;
using Convey.WebApi.Exceptions;
using Pacco.Services.Availability.Application.Commands.Handlers;
using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacco.Services.Availability.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMesssageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                MissingResourceTagsException ex => new AddResourceRejected(Guid.Empty, ex.Message, ex.Code),
                InvalidAggregateIdException ex => new AddResourceRejected(Guid.Empty, ex.Message, ex.Code),
                CannotExpropriateReservationException ex => new ResourceReservedRejected(ex.ResourceId, ex.Message, ex.Code),
                ResourceAlreadyExistsException ex => new AddResourceRejected(ex.ResourceId, ex.Message, ex.Code),
                ResourceNotFoundException ex => message switch
                {
                    ReserveResource cmd => new ResourceReservedRejected(cmd.ResourceId, ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
    }
}

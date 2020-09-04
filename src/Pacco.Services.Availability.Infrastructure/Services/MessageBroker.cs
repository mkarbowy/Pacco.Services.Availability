﻿using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Pacco.Services.Availability.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal sealed class MessageBroker : IMessageBroker
    {
        public MessageBroker(IBusPublisher busPublisher)
        {
            BusPublisher = busPublisher;
        }

        public IBusPublisher BusPublisher { get; }

        public Task PublishAsync(params IEvent[] events) => PublishAsync(events?.AsEnumerable());

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if(events is null)
            {
                return;
            }
            foreach (var @event in events)
            {
                if(@event is null)
                {
                    continue;
                }

                var messageId = Guid.NewGuid().ToString("N");
                await BusPublisher.PublishAsync(@event, messageId);
            }
        }
    }
}

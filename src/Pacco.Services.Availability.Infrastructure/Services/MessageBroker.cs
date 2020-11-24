using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenTracing;
using Pacco.Services.Availability.Application.Services;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal sealed class MessageBroker : IMessageBroker
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IMessageOutbox _outbox;
        private readonly OutboxOptions _outboxOptions;
        private readonly IMessagePropertiesAccessor _messagePropertiesAccessor;

        public MessageBroker(IBusPublisher busPublisher, IMessageOutbox outbox, OutboxOptions outboxOptions,
            IMessagePropertiesAccessor messagePropertiesAccessor)
        {
            _busPublisher = busPublisher;
            _outbox = outbox;
            _outboxOptions = outboxOptions;
            _messagePropertiesAccessor = messagePropertiesAccessor;
        }

        public Task PublishAsync(params IEvent[] events) => PublishAsync(events?.AsEnumerable());

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events is null)
            {
                return;
            }

            var messageProperties = _messagePropertiesAccessor.MessageProperties;
            var originatedMessageId = messageProperties?.MessageId;
            var correlationId = messageProperties?.CorrelationId;


            foreach (var @event in events)
            {
                if (@event is null)
                {
                    continue;
                }

                var messageId = Guid.NewGuid().ToString("N");
                
                if (_outbox.Enabled)
                {
                    await _outbox.SendAsync(@event, originatedMessageId, messageId);
                    continue;
                }

                await _busPublisher.PublishAsync(@event, messageId, correlationId);
            }
        }
    }
}
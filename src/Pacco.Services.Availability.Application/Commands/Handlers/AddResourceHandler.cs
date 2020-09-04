﻿using Convey.CQRS.Commands;
using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Commands.Handlers
{
    public class AddResourceHandler : ICommandHandler<AddResource>
    {
        private readonly IResourcesRepository _resourceRepository;
        private readonly IEventProcessor _eventProcessor;

        public AddResourceHandler(IResourcesRepository resourceRepository, IEventProcessor eventProcessor)
        {
            _resourceRepository = resourceRepository;
            _eventProcessor = eventProcessor;
        }
        public async Task HandleAsync(AddResource command)
        {
            if (await _resourceRepository.ExistsAsync(command.ResourceId))
            {
                throw new ResourceAlreadyExistsException(command.ResourceId);
            }

            var resource = Resource.Create(command.ResourceId, command.Tags);
            await _resourceRepository.AddAsync(resource);

            await _eventProcessor.ProcessAsync(resource.Events);
        }
    }
}

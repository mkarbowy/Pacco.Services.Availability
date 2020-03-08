using System;
using System.Collections.Generic;
using System.Text;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public class ResourceNotFoundException : AppException
    {
        public override string Code => "resource_not_found";

        public Guid ResourceId { get; }

        public ResourceNotFoundException(Guid resourceId) : base($"Resource with id: {resourceId} was not found.")
        {
            ResourceId = resourceId;
        }
    }
}

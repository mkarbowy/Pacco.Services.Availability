using Convey.CQRS.Commands;
using Pacco.Services.Availability.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Commands.Handlers
{
    public class ReserveResourceHandler : ICommandHandler<ReserveResource>
    {
        private readonly IResourcesRepository resourcesRepository;

        public ReserveResourceHandler(IResourcesRepository _resourcesRepository)
        {
            _resourcesRepository = _resourcesRepository;
        }

        public async Task HandleAsync(ReserveResource command)
        {
            throw new NotImplementedException();
        }
    }
}

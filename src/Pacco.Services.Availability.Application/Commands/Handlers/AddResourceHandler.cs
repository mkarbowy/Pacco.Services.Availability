using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Commands.Handlers
{
    public class AddResourceHandler : ICommandHandler<AddResource>
    {
        public Task HandleAsync(AddResource command)
        {
            return Task.CompletedTask;
        }
    }
}

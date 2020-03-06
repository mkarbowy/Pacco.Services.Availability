﻿using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacco.Services.Availability.Application.Commands.Handlers
{
    public class ReserveResource : ICommand
    {
        public ReserveResource(Guid resourceId, DateTime dateTime, int priority)
        {
            ResourceId = resourceId;
            DateTime = dateTime;
            Priority = priority;
        }

        public Guid ResourceId { get; }
        public DateTime DateTime { get; }
        public int Priority { get; }
    }
}

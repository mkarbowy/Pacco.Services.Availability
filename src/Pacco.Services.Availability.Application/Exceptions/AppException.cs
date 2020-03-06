using System;
using System.Collections.Generic;
using System.Text;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public abstract string Code { get; }

        protected AppException(string message): base(message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Gateways.Exceptions
{
    //todo: need to implement serialization
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message) : base(message)
        {

        }

        public ObjectNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ObjectNotFoundException() : base()
        {

        }
    }
}

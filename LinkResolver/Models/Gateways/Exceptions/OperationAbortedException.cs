using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Gateways.Exceptions
{
    //todo: need to implement serialization
    public class OperationAbortedException : Exception
    {
        public OperationAbortedException(string message) : base(message)
        {

        }

        public OperationAbortedException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }

        public OperationAbortedException() : base()
        {

        }
    }
}

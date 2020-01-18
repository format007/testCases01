using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.UseCases.Interfaces
{
    public class CommandResultError
    {
        public int HttpCode { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}

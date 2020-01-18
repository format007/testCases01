using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.UseCases.Interfaces
{
    public class CommandResult<T>
    {
        public bool Success { get; set; }
        public CommandResultError Error { get; set; }
        public T Result { get; set; }

        public CommandResult(bool success, CommandResultError error, T result)
        {
            Success = success;
            Error = error;
            Result = result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.UseCases.Interfaces
{
    public interface ICommandHandler<in TCommand, TCommandResult>
        where TCommand : ICommand<TCommandResult>
    {
        Task<CommandResult<TCommandResult>> ExecAsync(TCommand command);

    }
}

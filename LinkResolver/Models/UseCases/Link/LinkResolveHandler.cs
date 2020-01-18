using LinkResolver.Models.Dto.Requests;
using LinkResolver.Models.Gateways.Interfaces;
using LinkResolver.Models.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.UseCases.Link
{
    public class LinkResolveHandler : ICommandHandler<LinkResolveCommand, string>
    {
        private readonly ILinkManager linkMgr;

        public LinkResolveHandler(ILinkManager linkMgr)
        {
            this.linkMgr = linkMgr;
        }

        public async Task<CommandResult<string>> ExecAsync(LinkResolveCommand command)
        {
            string result = await linkMgr.Resolve(command.ShortUrl);
            return new CommandResult<string>(true, null, result);
        }
    }
}

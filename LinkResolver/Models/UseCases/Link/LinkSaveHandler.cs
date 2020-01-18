﻿using LinkResolver.Models.Dto.Requests;
using LinkResolver.Models.Gateways.Interfaces;
using LinkResolver.Models.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.UseCases.Link
{
    public class LinkSaveHandler : ICommandHandler<LinkSaveCommand, string>
    {
        private readonly ILinkManager linkMgr;

        public LinkSaveHandler(ILinkManager linkMgr)
        {
            this.linkMgr = linkMgr;
        }

        public async Task<CommandResult<string>> ExecAsync(LinkSaveCommand command)
        {
            string result = await linkMgr.Save(command.LongUrl);
            return new CommandResult<string>(true, null, result);
        }
    }
}

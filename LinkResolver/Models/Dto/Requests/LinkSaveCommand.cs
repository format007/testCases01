using LinkResolver.Models.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Dto.Requests
{
    public class LinkSaveCommand : ICommand<string>
    {
        public string LongUrl { get; set; }
    }
}

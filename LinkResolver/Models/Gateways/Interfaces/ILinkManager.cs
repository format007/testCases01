using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Gateways.Interfaces
{
    public interface ILinkManager
    {
        Task<string> Save(string LongUrl);
        Task<string> Resolve(string ShortUrl);
    }
}
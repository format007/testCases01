using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Gateways
{
    public class LinkManagerOptions
    {
        public string UrlBase { get; set; }
        public byte ShortMaxSize { get; set; } = 6;
        public int RetryLimit { get; set; } = 10;
    }
}

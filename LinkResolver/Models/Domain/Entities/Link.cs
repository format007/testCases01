using LinkResolver.Models.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Domain.Entities
{
    public class Link : IEntity<long>
    {
        public long Id { get; set; }
        public string LinkHash { get; set; }
        public string ShortLink { get; set; }
        public string LongLink { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

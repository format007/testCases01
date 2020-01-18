using LinkResolver.Models.Data.Interfaces;
using LinkResolver.Models.Domain.Entities;
using LinkResolver.Models.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Data.Repositories
{
    public interface ILinkEFManager : IEntityManager<Link, long>
    {

    }

    public class LinkEFManager: EntityManager<Link, long>, ILinkEFManager
    {
        public LinkEFManager(LinkDBContext ctx) : base(ctx)
        {

        }
    }
}

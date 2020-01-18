using LinkResolver.Models.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LinkResolver.Models.Domain.Entities
{
    public class LinkSpecification : BaseSpecification<Link>
    {
        public LinkSpecification(Expression<Func<Link, bool>> criteria)
            :base(criteria)
        {
        }
    }
}

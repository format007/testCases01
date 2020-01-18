using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Domain.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}

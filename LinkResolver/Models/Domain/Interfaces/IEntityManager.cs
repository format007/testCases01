using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Domain.Interfaces
{
    public interface IEntityManager<T, TId> where T : IEntity<TId>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(TId Id);
        Task<IEnumerable<T>> GetFiltered(ISpecification<T> predicate);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        System.Threading.Tasks.Task Delete(TId Id);
    }
}

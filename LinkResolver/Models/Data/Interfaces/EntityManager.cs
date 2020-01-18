using LinkResolver.Models.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Data.Interfaces
{
    public abstract class EntityManager<T, TId> : IEntityManager<T, TId> where T : class, IEntity<TId>, new()
    {
        protected LinkDBContext ctx;

        public EntityManager(LinkDBContext ctx) => this.ctx = ctx;

        public virtual async Task<T> Create(T entity)
        {
            await ctx.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async System.Threading.Tasks.Task Delete(TId Id)
        {
            ctx.Remove(new T { Id = Id });
            await ctx.SaveChangesAsync();
        }

        public virtual async Task<T> GetById(TId Id)
        {
            return await ctx.FindAsync<T>(Id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await ctx.Set<T>().ToListAsync();
        }

        public virtual async Task<T> Update(T entity)
        {
            ctx.Update<T>(entity);
            await ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetFiltered(ISpecification<T> predicate)
        {
            var result = ctx.Set<T>().AsQueryable();

            result = predicate.Includes.Aggregate(result,
                (current, include) => current.Include(include));

            result = predicate.IncludeStrings.Aggregate(result,
                (current, includeStr) => current.Include(includeStr));

            return await result.Where(predicate.Criteria).ToListAsync();
        }
    }
}
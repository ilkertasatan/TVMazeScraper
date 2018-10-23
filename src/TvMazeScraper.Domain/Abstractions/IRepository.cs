using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TvMazeScraper.Domain.SeedWork;

namespace TvMazeScraper.Domain.Abstractions
{
    public interface IRepository<T> where T : Entity
    {
        Task InsertAsync(T entity);
        Task InsertManyAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter);
        Task<long> CountAsync(Expression<Func<T, bool>> filter);
    }
}

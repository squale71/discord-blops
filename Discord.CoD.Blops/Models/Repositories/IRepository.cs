using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Models.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetOneByFilter(Expression<Func<T, object>> filter, object value);
        Task<IEnumerable<T>> GetManyByFilter(Expression<Func<T, object>> filter, object value);
        Task Upsert(T item);
        Task Delete(T item);
    }
}

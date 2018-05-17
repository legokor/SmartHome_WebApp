using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHome.Model
{
    public interface IRepository<T>
    {
        Task<bool> AddAsync(T newElement);

        Task<List<T>> FindListAsync(Expression<Func<T, bool>> queryLambda);

        Task<T> FindAsync(Expression<Func<T, bool>> queryLambda);

        Task<bool> ModifyAsync(T toModify);

        Task<bool> RemoveAsync(T toDelete);

        Task<bool> RemoveAsync(Guid toRemoveId);
    }
}

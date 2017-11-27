using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHome_WebApp.Data
{
    interface IRepository<T>
    {
        Task<bool> Add(T newElement);

        Task<List<T>> Find(Expression<Func<T, bool>> queryLambda);

        Task<bool> Modify(T toModify);

        Task<bool> Remove(T toDelete);

        Task<bool> Remove(Guid toRemoveId);
    }
}

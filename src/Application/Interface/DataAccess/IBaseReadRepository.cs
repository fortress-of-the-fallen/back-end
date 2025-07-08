using System.Linq.Expressions;
using Domain.IEntity;

namespace Application.Interface.DataAccess;

public interface IBaseReadRepository<T>
    where T : IBaseEntity
{
    IDeferredQuery<T> QueryCondition(Expression<Func<T, bool>> filter);

    IDeferredQuery<T> SortBy<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true);

    IDeferredQuery<T> Skip(int count);

    IDeferredQuery<T> Limit(int count);

    IDeferredQuery<T> Join<TForeign, TLocalKey, TForeignKey, TResult>(
        IBaseReadRepository<TForeign> from,
        Expression<Func<T, TLocalKey>> localField,
        Expression<Func<TForeign, TForeignKey>> foreignField,
        Expression<Func<T, IEnumerable<TForeign>, TResult>> resultSelector,
        string? asField = null
    ) where TForeign : IBaseEntity;

    Task<IEnumerable<T>> ToListAsync();
}

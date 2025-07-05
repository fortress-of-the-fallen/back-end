using System.Linq.Expressions;
using Domain.IEntity;

namespace Application.Interface.DataAccess;

public interface IBaseReadRepository<T>
    where T : IBaseEntity
{
    IBaseReadRepository<T> QueryCondition(Expression<Func<T, bool>> filter);

    IBaseReadRepository<T> SortBy<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true);

    IBaseReadRepository<T> Skip(int count);

    IBaseReadRepository<T> Limit(int count);

    IBaseReadRepository<T> Join<TForeign, TLocalKey, TForeignKey, TResult>(
        IBaseReadRepository<TForeign> from,
        Expression<Func<T, TLocalKey>> localField,
        Expression<Func<TForeign, TForeignKey>> foreignField,
        Expression<Func<T, IEnumerable<TForeign>, TResult>> resultSelector,
        string? asField = null
    ) where TForeign : IBaseEntity;

    IEnumerable<T> ToList();
}

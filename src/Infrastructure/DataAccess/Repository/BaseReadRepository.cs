using System.Linq.Expressions;
using Application.Interface.DataAccess;
using Infrastructure.DataAccess.DbContext;
using MongoDB.Driver;
namespace Infrastructure.DataAccess.Repository;
using Domain.IEntity;
using Infrastructure.DataAccess.Deferred;
using MongoDB.Bson;

public class BaseReadRepository<T> : IBaseReadRepository<T>
    where T : IBaseEntity, IIsDeletedEntity
{
    private readonly IDeferredQuery<T> _deferredQuery;

    private readonly BaseDbContext _context;

    public BaseReadRepository(BaseDbContext context)
    {
        _context = context;
        _deferredQuery = new DeferredQuery<T>(context);
    }

    public IDeferredQuery<T> QueryCondition(Expression<Func<T, bool>> filter)
    {
        return _deferredQuery.QueryCondition(filter);
    }

    public IDeferredQuery<T> Join<TForeign, TLocalKey, TForeignKey, TResult>(
        IBaseReadRepository<TForeign> from, 
        Expression<Func<T, TLocalKey>> localField, 
        Expression<Func<TForeign, TForeignKey>> foreignField, 
        Expression<Func<T, IEnumerable<TForeign>, TResult>> resultSelector, 
        string? asField = null
    ) where TForeign : IBaseEntity
    {
        return _deferredQuery.Join(from, localField, foreignField, resultSelector, asField);
    }

    public IDeferredQuery<T> Limit(int count)
    {
        return _deferredQuery.Limit(count);
    }

    public IDeferredQuery<T> Skip(int count)
    {
        return _deferredQuery.Skip(count);
    }

    public IDeferredQuery<T> SortBy<TKey>(
        Expression<Func<T, TKey>> keySelector, 
        bool ascending = true
    )
    {
        return _deferredQuery.SortBy(keySelector, ascending);
    }

    public async Task<IEnumerable<T>> ToListAsync()
    {
        return await _deferredQuery.ToListAsync();
    }
}
using System.Linq.Expressions;
using Application.Interface.DataAccess;
using DnsClient.Internal;
using Domain.IEntity;
using Infrastructure.DataAccess.DbContext;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.Deferred;

public class DeferredQuery<T> : IDeferredQuery<T>
{
    protected IEnumerable<BsonDocument> _pipeline = new List<BsonDocument>();

    protected IMongoCollection<T> _collection;

    public DeferredQuery(BaseDbContext context)
    {
        _collection = context.GetCollection<T>();
    }

    public IDeferredQuery<T> Join<TForeign, TLocalKey, TForeignKey, TResult>(IBaseReadRepository<TForeign> from, Expression<Func<T, TLocalKey>> localField, Expression<Func<TForeign, TForeignKey>> foreignField, Expression<Func<T, IEnumerable<TForeign>, TResult>> resultSelector, string? asField = null) where TForeign : IBaseEntity
    {
        string localFieldName = GetMemberName(localField);
        string foreignFieldName = GetMemberName(foreignField);
        string foreignCollectionName = typeof(TForeign).Name;
        string asFieldName = asField ?? typeof(TForeign).Name;

        var letVariableName = "localValue";

        var lookup = new BsonDocument("$lookup", new BsonDocument
        {
            { "from", foreignCollectionName },
            { "let", new BsonDocument { { letVariableName, $"${localFieldName}" } } },
            { "pipeline", new BsonArray {
                new BsonDocument("$match", new BsonDocument {
                    { "$expr", new BsonDocument("$and", new BsonArray {
                        new BsonDocument("$eq", new BsonArray { $"${foreignFieldName}", $"$${letVariableName}" }),
                        new BsonDocument("$eq", new BsonArray { "$IsDeleted", false })
                    }) }
                })
            } },
            { "as", asFieldName }
        });

        _pipeline.Append(lookup);

        return this;
    }

    public IDeferredQuery<T> Limit(int count)
    {
        var limitStage = new BsonDocument("$limit", count);
        _pipeline.Append(limitStage);

        return this;
    }

    public IDeferredQuery<T> QueryCondition(Expression<Func<T, bool>> filter)
    {
        Expression<Func<T, bool>> combinedFilter = filter;

        if (typeof(IIsDeletedEntity).IsAssignableFrom(typeof(T)))
        {
            var param = filter.Parameters[0];
            var isDeletedProp = Expression.Property(param, nameof(IIsDeletedEntity.IsDeleted));
            var notDeleted = Expression.Not(isDeletedProp);
            var isNotDeletedExpr = Expression.Lambda<Func<T, bool>>(notDeleted, param);

            combinedFilter = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(filter.Body, isNotDeletedExpr.Body),
                param
            );
        }

        var mongoFilter = Builders<T>.Filter.Where(combinedFilter);

        var renderedFilter = mongoFilter.Render(
            new RenderArgs<T>(
                _collection.DocumentSerializer,
                _collection.Settings.SerializerRegistry
            )
        );

        _pipeline.Append(new BsonDocument("$match", renderedFilter));
        return this;
    }

    public IDeferredQuery<T> Skip(int count)
    {
        var skipStage = new BsonDocument("$skip", count);
        _pipeline.Append(skipStage);

        return this;
    }

    public IDeferredQuery<T> SortBy<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true)
    {
        string fieldName = GetMemberName(keySelector);
        int sortOrder = ascending ? 1 : -1;

        var sortStage = new BsonDocument("$sort", new BsonDocument
        {
            { fieldName, sortOrder }
        });

        _pipeline.Append(sortStage);

        return this;
    }

    public async Task<IEnumerable<T>> ToListAsync()
    {
        var result = await _collection.Aggregate<T>(_pipeline.ToList()).ToListAsync();
        return result;
    }
    
    private string GetMemberName<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
    {
        if (expression.Body is MemberExpression member)
            return member.Member.Name;

        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression memberExpr)
            return memberExpr.Member.Name;

        throw new InvalidOperationException("Could not extract field name from expression.");
    }
}
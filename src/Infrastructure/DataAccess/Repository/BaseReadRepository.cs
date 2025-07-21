namespace Infrastructure.DataAccess.Repository;

using System.Linq.Expressions;
using Application.Interface.DataAccess;
using Infrastructure.DataAccess.DbContext;
using Domain.IEntity;
using MongoDB.Bson;
using MongoDB.Driver;

public class BaseReadRepository<T> : IBaseReadRepository<T>
    where T : IBaseEntity
{
    protected IEnumerable<BsonDocument> _pipeline = new List<BsonDocument>();

    protected IMongoCollection<T> _collection;

    public BaseReadRepository(BaseDbContext context, bool isTimeToLive = false)
    {
        _collection = context.GetCollection<T>();

        if (isTimeToLive)
        {
            var indexKeys = Builders<T>.IndexKeys.Ascending("ExpireAt");
            var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };
            _collection.Indexes.CreateOne(new CreateIndexModel<T>(indexKeys, indexOptions));
        }
    }

    public IBaseReadRepository<T> Join<TForeign, TLocalKey, TForeignKey, TResult>(
        IBaseReadRepository<TForeign> from, 
        Expression<Func<T, TLocalKey>> localField, 
        Expression<Func<TForeign, TForeignKey>> foreignField, 
        Expression<Func<T, IEnumerable<TForeign>, TResult>> resultSelector, string? asField = null) 
            where TForeign : IBaseEntity
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

        _pipeline = _pipeline.Append(lookup);

        return this;
    }

    public IBaseReadRepository<T> Limit(int count)
    {
        var limitStage = new BsonDocument("$limit", count);
        _pipeline = _pipeline.Append(limitStage);

        return this;
    }

    public async Task<T?> Single(Expression<Func<T, bool>> filter)
    {
        var parameter = filter.Parameters[0];

        var isDeleteProp = Expression.Property(parameter, "IsDeleted");
        var isDeleteNotTrue = Expression.NotEqual(
            isDeleteProp,
            Expression.Constant(true, typeof(bool))
        );

        var combinedBody = Expression.AndAlso(filter.Body, isDeleteNotTrue);
        var combined = Expression.Lambda<Func<T, bool>>(combinedBody, parameter);

        return await _collection.Find(combined).SingleOrDefaultAsync();
    }

    public IBaseReadRepository<T> QueryCondition(Expression<Func<T, bool>> filter)
    {
        Expression<Func<T, bool>> combinedFilter = filter;

        var param = filter.Parameters[0];
        var isDeletedProp = Expression.Property(param, nameof(IBaseEntity.IsDeleted));
        var notDeleted = Expression.Not(isDeletedProp);
        var isNotDeletedExpr = Expression.Lambda<Func<T, bool>>(notDeleted, param);

        combinedFilter = Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(filter.Body, isNotDeletedExpr.Body),
            param
        );

        var mongoFilter = Builders<T>.Filter.Where(combinedFilter);

        var renderedFilter = mongoFilter.Render(
            new RenderArgs<T>(
                _collection.DocumentSerializer,
                _collection.Settings.SerializerRegistry
            )
        );

        _pipeline = _pipeline.Append(new BsonDocument("$match", renderedFilter));
        return this;
    }

    public IBaseReadRepository<T> Skip(int count)
    {
        var skipStage = new BsonDocument("$skip", count);
        _pipeline = _pipeline.Append(skipStage);

        return this;
    }

    public IBaseReadRepository<T> SortBy<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true)
    {
        string fieldName = GetMemberName(keySelector);
        int sortOrder = ascending ? 1 : -1;

        var sortStage = new BsonDocument("$sort", new BsonDocument
        {
            { fieldName, sortOrder }
        });

        _pipeline = _pipeline.Append(sortStage);

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
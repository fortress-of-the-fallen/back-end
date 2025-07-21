using Application.Interface.DataAccess;
using Domain.IEntity;
using Infrastructure.DataAccess.DbContext;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.Repository;

public class BaseWriteRepository<T> : BaseReadRepository<T>, IBaseWriteRepository<T>
    where T : IBaseEntity
{
    private IEnumerable<WriteModel<T>> _commands;

    public BaseWriteRepository(
        BaseDbContext context,
        IEnumerable<WriteModel<T>> commands) : base(context)
    {
        _commands = commands;
    }

    public IBaseWriteRepository<T> HardDelete(T entity)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        _commands = _commands.Append(new DeleteOneModel<T>(filter));

        return this;
    }

    public IBaseWriteRepository<T> Insert(T entity)
    {
        _commands = _commands.Append(new InsertOneModel<T>(entity));

        return this;
    }

    public IBaseWriteRepository<T> Restore(T entity)
    {
        if (!entity.IsDeleted)
        {
            throw new Exception("Entity is not deleted");
        }

        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        var update = Builders<T>.Update.Set(x => x.IsDeleted, false);
        _commands = _commands.Append(new UpdateOneModel<T>(filter, update));

        return this;
    }

    public IBaseWriteRepository<T> SoftDelete(T entity)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        var update = Builders<T>.Update.Set(x => x.IsDeleted, true);
        _commands = _commands.Append(new UpdateOneModel<T>(filter, update));

        return this;
    }

    public IBaseWriteRepository<T> Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        
        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        var replaceOne = new ReplaceOneModel<T>(filter, entity);
        _commands = _commands.Append(replaceOne);

        return this;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (_commands.Count() == 0) return;

        await _collection.BulkWriteAsync(_commands, cancellationToken: cancellationToken);
        _commands = new List<WriteModel<T>>();
    }
}
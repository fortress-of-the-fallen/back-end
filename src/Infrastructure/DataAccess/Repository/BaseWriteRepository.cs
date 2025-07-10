using Application.Interface.DataAccess;
using Domain.IEntity;
using Infrastructure.DataAccess.DbContext;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.Repository;

public class BaseWriteRepository<T> : BaseReadRepository<T>, IBaseWriteRepository<T>
    where T : IBaseEntity, IIsDeletedEntity
{
    private IEnumerable<WriteModel<T>> _commands = new List<WriteModel<T>>();

    public BaseWriteRepository(BaseDbContext context) : base(context) { }

    public async Task<IBaseWriteRepository<T>> ExecuteAsync()
    {
        if (_commands.Count() == 0) return this;

        await _collection.BulkWriteAsync(_commands);
        _commands = new List<WriteModel<T>>();

        return this;
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
        if (entity is not IIsDeletedEntity)
        {
            throw new Exception("Entity is not IIsDeletedEntity");
        }

        if (entity.IsDeleted)
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
        if (entity is not IIsDeletedEntity)
        {
            throw new Exception("Entity is not IIsDeletedEntity");
        }

        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        var update = Builders<T>.Update.Set(x => x.IsDeleted, true);
        _commands = _commands.Append(new UpdateOneModel<T>(filter, update));

        return this;
    }

    public IBaseWriteRepository<T> Update(T entity)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        var update = Builders<T>.Update.Set(x => x.UpdatedAt, DateTime.UtcNow);
        _commands = _commands.Append(new UpdateOneModel<T>(filter, update));

        return this;
    }
}
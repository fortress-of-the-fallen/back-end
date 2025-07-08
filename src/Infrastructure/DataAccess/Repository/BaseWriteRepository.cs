using Application.Interface.DataAccess;
using Domain.IEntity;
using Infrastructure.DataAccess.DbContext;
using Infrastructure.DataAccess.Deferred;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.Repository;

public class BaseWriteRepository<T> : BaseReadRepository<T>, IBaseWriteRepository<T>
    where T : IBaseEntity, IIsDeletedEntity
{
    private readonly List<WriteModel<T>> _commands;

    public BaseWriteRepository(BaseDbContext context, List<WriteModel<T>> commands) : base(context)
    {
        _commands = commands;
    }

    public async Task ExecuteAsync()
    {
        if (_commands.Count == 0) return;

        await _collection.BulkWriteAsync(_commands);
        _commands.Clear();
    }

    public void HardDelete(T entity)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        _commands.Add(new DeleteOneModel<T>(filter));
    }

    public void Insert(T entity)
    {
        _commands.Add(new InsertOneModel<T>(entity));
    }

    public void Restore(T entity)
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
        _commands.Add(new UpdateOneModel<T>(filter, update));
    }

    public void SoftDelete(T entity)
    {
        if (entity is not IIsDeletedEntity)
        {
            throw new Exception("Entity is not IIsDeletedEntity");
        }

        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        var update = Builders<T>.Update.Set(x => x.IsDeleted, true);
        _commands.Add(new UpdateOneModel<T>(filter, update));
    }

    public void Update(T entity)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        var update = Builders<T>.Update.Set(x => x.UpdatedAt, DateTime.UtcNow);
        _commands.Add(new UpdateOneModel<T>(filter, update));
    }
}
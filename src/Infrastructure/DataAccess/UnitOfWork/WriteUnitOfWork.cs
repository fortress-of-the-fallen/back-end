using Application.Interface.DataAccess;
using Domain.IEntity;
using Infrastructure.DataAccess.DbContext;
using Infrastructure.DataAccess.Repository;

namespace Infrastructure.DataAccess.UnitOfWork;

public class WriteUnitOfWork(
    BaseDbContext context
)
{
    private readonly Dictionary<Type, object> _repositories = new();

    public IBaseWriteRepository<T> GetRepository<T>()
        where T : IBaseEntity, IIsDeletedEntity
    {
        var type = typeof(BaseWriteRepository<T>);

        if (!_repositories.TryGetValue(type, out var value))
        {
            value = new BaseWriteRepository<T>(context);
            _repositories[type] = value;
        }

        return (IBaseWriteRepository<T>)value;
    }
}
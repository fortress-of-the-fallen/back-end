using Application.Interface.DataAccess;
using Domain.IEntity;
using Infrastructure.DataAccess.DbContext;
using Infrastructure.DataAccess.Repository;

namespace Infrastructure.DataAccess.UnitOfWork;

public class ReadUnitOfWork(
    BaseDbContext context
)
{
    private readonly Dictionary<Type, object> _repositories = new();

    public IBaseReadRepository<T> GetRepository<T>()
        where T : IBaseEntity, IIsDeletedEntity
    {
        var type = typeof(BaseReadRepository<T>);

        if (!_repositories.TryGetValue(type, out var value))
        {
            value = new BaseReadRepository<T>(context);
            _repositories[type] = value;
        }

        return (IBaseReadRepository<T>)value;
    }
}
using Domain.IEntity;

namespace Application.Interface.DataAccess;

public interface IReadUnitOfWork
{
    IBaseReadRepository<T> GetRepository<T>()  where T : IBaseEntity;
}
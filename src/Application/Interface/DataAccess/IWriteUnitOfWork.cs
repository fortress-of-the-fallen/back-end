using Domain.IEntity;

namespace Application.Interface.DataAccess;

public interface IWriteUnitOfWork
{
    IBaseWriteRepository<T> GetRepository<T>() where T : IBaseEntity;
}
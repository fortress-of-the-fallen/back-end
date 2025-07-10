using Domain.IEntity;

namespace Application.Interface.DataAccess;

public interface IBaseWriteRepository<T>
    where T : IBaseEntity, IIsDeletedEntity
{
    IBaseWriteRepository<T> Insert(T entity);

    IBaseWriteRepository<T> Update(T entity);

    IBaseWriteRepository<T> SoftDelete(T entity);

    IBaseWriteRepository<T> HardDelete(T entity);

    IBaseWriteRepository<T> Restore(T entity);

    Task<IBaseWriteRepository<T>> ExecuteAsync();
}
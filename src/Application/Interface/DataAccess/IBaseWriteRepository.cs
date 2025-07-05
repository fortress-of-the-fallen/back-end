using Domain.IEntity;

namespace Application.Interface.DataAccess;

public interface IBaseWriteRepository<T>
    where T : IBaseEntity, IIsDeletedEntity
{
    void Insert(T entity);

    void Update(T entity);

    void SoftDelete(T entity);

    void HardDelete(T entity);

    void Restore(T entity);

    Task ExecuteAsync();
}
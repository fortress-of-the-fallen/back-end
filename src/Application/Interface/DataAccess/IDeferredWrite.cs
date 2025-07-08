using Application.Interface.DataAccess;

namespace Infrastructure.DataAccess.Repository;

public interface IDeferredWrite<T> : IDeferredQuery<T>
{
    void Insert(T entity);

    void Update(T entity);

    void SoftDelete(T entity);

    void Restore(T entity);

    void HardDelete(T entity);

    Task ExecuteAsync();
}
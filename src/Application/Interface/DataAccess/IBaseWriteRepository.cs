using Domain.IEntities;

namespace Application.Interface.DataAccess;

public interface IBaseWriteRepository<T>
    where T : IBaseEntity
{

}
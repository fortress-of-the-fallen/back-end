using Domain.IEntities;

namespace Application.Interface.DataAccess;

public interface IBaseReadRepository<T>
    where T : IBaseEntity
{

}

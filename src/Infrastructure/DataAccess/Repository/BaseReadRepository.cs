using Application.Interface.DataAccess;
using Domain.IEntities;
using Infrastructure.DataAccess.DbContext;
using MongoDB.Driver;
namespace Infrastructure.DataAccess.Repository;

public class BaseReadRepository<T> : IBaseReadRepository<T>
    where T : IBaseEntity
{
    private readonly GameDbContext<T> _context;

    public BaseReadRepository(GameDbContext<T> context)
    {
        _context = context;
    }

    public IMongoCollection<T> GetCollection()
    {
        return _context.GetCollection<T>();
    }

    public IFindFluent<T, T> Find(Expression<Func<T, bool>> filter)
    {
        return _context.GetCollection<T>().Find(filter);
    }
}
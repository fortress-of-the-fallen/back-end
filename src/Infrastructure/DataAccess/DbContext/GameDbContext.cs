using Domain.IEntities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.DbContext;

public class GameDbContext<T> : BaseDbContext 
    where T : IBaseEntity
{
    public GameDbContext(IMongoClient client, IConfiguration configuration) : base(client, configuration)
    {
        _database = client.GetDatabase(configuration["MongoDbSettings:GameDatabase"]);
    }
}
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.DbContext;

public class BaseDbContext
{
    protected IMongoDatabase _database;

    protected IConfiguration _configuration;

    public BaseDbContext(IMongoClient client, IConfiguration configuration)
    {
        _configuration = configuration;
        _database = client.GetDatabase(configuration["MongoDbSettings:BaseDatabase"]);
    }

    public IMongoCollection<T> GetCollection<T>()
    {
        return _database.GetCollection<T>(typeof(T).Name.ToLower());
    }
}
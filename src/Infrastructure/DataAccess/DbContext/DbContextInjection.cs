using Domain.Constains;
using Infrastructure.DataAccess.DbContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.DbContext;

public static class DbContextInjection
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration[ConfigKeys.MongoDbSettings.ConnectionString];
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        var client = new MongoClient(settings);
        client.ListDatabaseNames().ToList();
        services.AddSingleton<IMongoClient>(sp => client);
        services.AddScoped<BaseDbContext>();
    }
}
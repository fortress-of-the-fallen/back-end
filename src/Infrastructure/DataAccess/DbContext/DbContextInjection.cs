using Domain.Constains;
using Infrastructure.DataAccess.DbContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.DbContext;

public static class DbContextInjection
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(sp => new MongoClient(configuration[ConfigKeys.MongoDbSettings.ConnectionString]));
        services.AddScoped<BaseDbContext>();
    }
}
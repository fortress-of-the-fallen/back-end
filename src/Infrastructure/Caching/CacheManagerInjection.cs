using Application.Interface.Caching;
using Domain.Constains;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Caching;

public static class CacheManagerInjection
{
    public static void AddRedis(this IServiceCollection collection, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection(ConfigKeys.Redis.Connection).Value;

        collection.AddSingleton<IConnectionMultiplexer>(p => ConnectionMultiplexer.Connect(connectionString ?? string.Empty));

        collection.AddScoped<ICacheManager, CacheManager>();
    }
}
using Application.Interface.Broadcast;
using Domain.Constains;
using Infrastructure.RealTime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Realtime;

public static class RealTimeInjection
{
    public static void AddRealTimeServices(this IServiceCollection collection, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetSection(ConfigKeys.Redis.ConnectionString).Get<string>();
        if (string.IsNullOrEmpty(redisConnectionString))
        {
            throw new ArgumentNullException(nameof(redisConnectionString));
        }

        collection.AddSignalR().AddStackExchangeRedis(redisConnectionString, options =>
        {
            options.Configuration.ChannelPrefix = RedisChannel.Literal("AFusion.Orchestrator.Hub_");
        });

        collection.AddScoped<IBroadcastMessageService, BroadcastMessageService>();
    }

    public static void AddHub(this WebApplication app)
    {
        app.MapHub<AuthHub>("/hubs/auth", options =>
        {
            options.CloseOnAuthenticationExpiration = false;
        });
    }
}
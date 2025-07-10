using Infrastructure.Caching;
using Infrastructure.DataAccess;
using Infrastructure.MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class AddCoreDenpendencies
{
    public static void AddDenpendencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRedis(configuration);
        services.AddDataAccess(configuration);
        services.AddMediatR();
    }
}
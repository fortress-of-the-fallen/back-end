using Application.Feature.Auth;
using Infrastructure.Caching;
using Infrastructure.DataAccess;
using Infrastructure.IdGenerator;
using Infrastructure.MediatR;
using Infrastructure.Restful;
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
        services.AddRestfulService();
        services.AddIdGenerator();
        services.AddFeatureInjection();
    }
}
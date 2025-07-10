using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess;

public static class DataAccessInjection
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddScoped<IDataAccess, DataAccess>();
    }
}
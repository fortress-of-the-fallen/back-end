using Application.Interface.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.UnitOfWork;

public static class UoWInjection
{
    public static void AddUoW(this IServiceCollection services)
    {
        services.AddScoped<IReadUnitOfWork, ReadUnitOfWork>();
        services.AddScoped<IWriteUnitOfWork, WriteUnitOfWork>();
    }
}
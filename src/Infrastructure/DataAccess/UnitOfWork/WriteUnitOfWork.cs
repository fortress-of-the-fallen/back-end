using Application.Interface.DataAccess;
using DnsClient.Internal;
using Domain.IEntity;
using Infrastructure.DataAccess.DbContext;
using Infrastructure.DataAccess.Repository;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.UnitOfWork;

public class WriteUnitOfWork(
    BaseDbContext context,
    ILogger<WriteUnitOfWork> logger
) : IWriteUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public IBaseWriteRepository<T> GetRepository<T>()
        where T : IBaseEntity
    {
        var type = typeof(BaseWriteRepository<T>);

        if (!_repositories.TryGetValue(type, out var value))
        {
            value = new BaseWriteRepository<T>(context, new List<WriteModel<T>>());
            _repositories[type] = value;
        }

        return (IBaseWriteRepository<T>)value;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        foreach (var repository in _repositories.Values)
        {
            var method = repository.GetType().GetMethod("ExecuteAsync");
            if (method != null)
            {
                try
                {
                    var result = method.Invoke(repository, new object?[] { cancellationToken });

                    if (result is Task task)
                    {
                        await task;
                    }
                    else
                    {
                        logger.LogWarning("ExecuteAsync method did not return a Task.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing repository operation");
                    throw;
                }
            }
        }
    }
}
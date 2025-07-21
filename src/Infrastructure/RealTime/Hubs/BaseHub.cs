using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.RealTime.Hubs;

public interface IBaseHub
{
    /// <summary>
    ///     Base broadcast method
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    /// <param name="message">Data</param>
    Task Broadcast<T>(T message);
}

public class BaseHub<T> : Hub<T> where T : class, IBaseHub
{
}
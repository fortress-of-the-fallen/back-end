using Application.Interface.Caching;
using Infrastructure.RealTime.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.RealTime;

public interface IAuthHub : IBaseHub
{
}

public class AuthHub : BaseHub<IAuthHub>
{
    public async Task JoinLoginSessionChannel(string sessionId)
    {
        var connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, sessionId);
    }

    public async Task LeaveLoginSessionChannel(string sessionId)
    {
        var connectionId = Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(connectionId, sessionId);
    }
}
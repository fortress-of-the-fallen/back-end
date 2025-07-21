using System.Diagnostics;
using System.Linq.Expressions;
using Application.Dto.RealTime;
using Application.Interface.Broadcast;
using Domain.Constants;
using Domain.Helpers;
using Infrastructure.RealTime;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;

namespace Infrastructure.Realtime;

public class BroadcastMessageService(
    IHubContext<AuthHub, IAuthHub> authHub
) : IBroadcastMessageService
{
    public Task SendMessageAsync<T>(string id, BroadcastMessage<T> message) where T : class
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageToGroupAsync<T>(string groupName, BroadcastMessage<T> message) where T : class
    {
        switch (message.Action)
        {
            case RealTimeConstants.Actions.JoinLoginSession:
                if(message.Data is not LoginSessionDto)
                    throw new ArgumentException("Invalid login session data");

                await authHub.Clients.Group(groupName).Broadcast(message.ToJson());
                break;
        }
    }
}
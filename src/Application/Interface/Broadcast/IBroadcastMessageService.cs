namespace Application.Interface.Broadcast;

public interface IBroadcastMessageService
{
    Task SendMessageAsync<T>(string id, BroadcastMessage<T> message) where T : class;

    Task SendMessageToGroupAsync<T>(string groupName, BroadcastMessage<T> message) where T : class;
}
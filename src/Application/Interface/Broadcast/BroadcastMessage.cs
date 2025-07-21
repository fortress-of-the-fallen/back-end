namespace Application.Interface.Broadcast;

public class BroadcastMessage
{
    public string? Action { get; set; }
}

public class BroadcastMessage<T> : BroadcastMessage
{
    public T? Data { get; set; }
}
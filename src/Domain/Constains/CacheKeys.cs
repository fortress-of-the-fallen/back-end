namespace Domain.Constants;

public static class CacheKeys
{
    public static class Session
    {
        public static string SessionId(string sessionId) => $"session:{sessionId}";
    }
}
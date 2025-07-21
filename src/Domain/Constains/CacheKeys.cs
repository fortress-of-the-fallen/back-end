namespace Domain.Constants;

public static class CacheKeys
{
    public struct Session
    {
        public const int IdLength = 15;
        public const int SessionDurationHours = 1;
        public static string SessionId(string id) => $"session-{id}";
    }

    public struct Login
    {
        public static string LoginSessionId(string id) => $"login-session-{id}";
        public const int LoginDurationMinutes = 2;
    }
}
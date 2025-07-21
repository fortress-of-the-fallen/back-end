namespace Domain.Message;

public static class AuthControllerMsg
{
    public struct Login
    {
        public const string InvalidGithubCode = "Login.InvalidGithubCode";

        public const string UserAlreadyLoggedIn = "Login.UserAlreadyLoggedIn";
    }
}
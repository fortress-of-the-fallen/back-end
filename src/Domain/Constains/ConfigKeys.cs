namespace Domain.Constains;

public static class ConfigKeys
{
    public struct Redis
    {
        public const string ConnectionString = "Redis:ConnectionString";
    }

    public struct MongoDbSettings
    {
        public const string ConnectionString = "MongoDbSettings:ConnectionString";
        public const string DefaultDatabase = "MongoDbSettings:DefaultDatabase";
        public const string GameDatabase = "MongoDbSettings:GameDatabase";
    }

    public struct Authorization
    {
        public struct GithubSSO
        {
            public const string ClientId = "Authorization:GithubSSO:ClientId";
            public const string ClientSecret = "Authorization:GithubSSO:ClientSecret";
            public const string CallbackUrl = "Authorization:GithubSSO:CallbackUrl";
            public const string AccessTokenUrl = "Authorization:GithubSSO:AccessTokenUrl";
            public const string UserApiUrl = "Authorization:GithubSSO:UserApiUrl";
        }
    }
}
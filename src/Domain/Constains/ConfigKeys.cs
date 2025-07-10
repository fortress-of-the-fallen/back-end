namespace Domain.Constains;

public static class ConfigKeys
{
    public static class Redis
    {
        public const string ConnectionString = "Redis:ConnectionString";
    }

    public static class MongoDbSettings
    {
        public const string ConnectionString = "MongoDbSettings:ConnectionString";
        public const string DefaultDatabase = "MongoDbSettings:DefaultDatabase";
        public const string GameDatabase = "MongoDbSettings:GameDatabase";
    }
}
namespace Domain.Constains;

public static class ConfigKeys
{
    public static class Redis
    {
        public const string Connection = "Redis:Connection";
        public const string InstanceName = "Redis:InstanceName";
        public const string ConnectRetry = "Redis:ConnectRetry";
        public const string ConnectTimeout = "Redis:ConnectTimeout";
    }

    public static class MongoDbSettings
    {
        public const string ConnectionString = "MongoDbSettings:ConnectionString";
        public const string DefaultDatabase = "MongoDbSettings:DefaultDatabase";
        public const string GameDatabase = "MongoDbSettings:GameDatabase";
    }
}
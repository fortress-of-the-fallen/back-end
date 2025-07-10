namespace Application.Interface.Caching;

public interface ICacheManager
{
    Task<T?> GetData<T>(string key);

    Task<bool> SetData<T>(string key, T value, TimeSpan duration);

    Task<bool> RemoveCache(string key);
}
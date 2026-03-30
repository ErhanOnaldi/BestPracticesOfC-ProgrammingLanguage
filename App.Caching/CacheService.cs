using App.Application.Interfaces.Caching;

namespace App.Caching;

public class CacheService : ICacheService
{
    public Task<T> GetAsync<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string key)
    {
        throw new NotImplementedException();
    }
}
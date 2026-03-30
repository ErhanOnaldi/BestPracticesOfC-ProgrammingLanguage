using App.Application.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    public Task<T?> GetAsync<T>(string key)
    {
        return memoryCache.TryGetValue(key, out T cacheValue) ? Task.FromResult( cacheValue ) : Task.FromResult<T>( default(T) );
    }

    public Task<T> AddAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
        memoryCache.Set(key, value, cacheOptions);
        return Task.FromResult(value);
    }

    public Task RemoveAsync(string key)
    {
        memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}
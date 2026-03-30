namespace App.Application.Interfaces.Caching;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key);
    Task<T> AddAsync<T>(string key, T value,  TimeSpan? expiration = null);
    Task RemoveAsync(string key);
}
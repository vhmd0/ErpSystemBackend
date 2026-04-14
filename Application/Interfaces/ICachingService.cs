using System;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICachingService
{
    Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<T?> GetCacheAsync<T>(string key);
    Task RemoveCacheAsync(string key);
}

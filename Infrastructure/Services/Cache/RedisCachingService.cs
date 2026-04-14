using System.Text.Json;
using Application.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services.Cache;

public class RedisCachingService(IConnectionMultiplexer redis) : ICachingService
{
    private readonly IDatabase _cacheDb = redis.GetDatabase();

    public async Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        await _cacheDb.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetCacheAsync<T>(string key)
    {
        var cachedValue = await _cacheDb.StringGetAsync(key);
        if (cachedValue.HasValue)
        {
            // Explicitly cast to string to resolve ambiguity
            return JsonSerializer.Deserialize<T>(cachedValue!.ToString());
        }
        return default;
    }

    public async Task RemoveCacheAsync(string key)
    {
        await _cacheDb.KeyDeleteAsync(key);
    }
}

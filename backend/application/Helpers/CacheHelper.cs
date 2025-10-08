using Microsoft.Extensions.Caching.Memory;

namespace application.Helpers
{
    public static class CacheHelper
    {
        public static async Task<T> GetOrCreateAsync<T>(
            IMemoryCache cache,
            string key,
            Func<Task<T>> factory,
            TimeSpan expiration)
        {
            var result = await cache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = expiration;
                var value = await factory();
                return value;
            });

            return result;
        }
    }
}

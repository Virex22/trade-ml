using App.Entity;
using System.Runtime.Caching;

namespace App.Provider
{
    public static class NetCacheDataProvider
    {
        private static readonly ObjectCache cache = MemoryCache.Default;
        private static readonly TimeSpan cacheDuration = TimeSpan.FromDays(7);

        public static List<Candle>? GetCache(string key)
        {
            if (!cache.Contains(key))
            {
                if (ConfigProvider.GetConfig().consoleMessage.cache)
                    Console.WriteLine("Cache not found for key: " + key);
                return null;
            }

            if (ConfigProvider.GetConfig().consoleMessage.cache)
                Console.WriteLine("Successfully loaded cache for key: " + key);

            return (List<Candle>)cache[key];
        }

        public static void SetCache(string key, List<Candle> candles)
        {
            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.Add(cacheDuration)
            };

            cache.Set(key, candles, policy);
        }
    }
}

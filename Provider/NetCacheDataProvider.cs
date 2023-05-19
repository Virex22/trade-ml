using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return null;

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

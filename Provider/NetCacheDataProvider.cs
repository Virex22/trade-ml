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
        public static List<Candle>? GetCache(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            if (!cache.Contains(key))
                return null;
            return (List<Candle>)cache[key];
        }

        public static void SetCache(string key, List<Candle> candles)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);
            cache.Set(key, candles, policy);
        }
    }
}

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
    }
}

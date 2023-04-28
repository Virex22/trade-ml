using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Enumerator;

namespace App.Provider
{
    public class CacheDataProvider
    {
        private static CacheDataProvider? _instance;

        public static CacheDataProvider getInstance()
        {
            return new CacheDataProvider();
        }

        private static CacheDataProvider GetInstance()
        {
            if (_instance == null)
                _instance = new CacheDataProvider();
            return _instance;
        }

        public List<Candle>? GetCache(string key)
        {
            Config config = Config.GetInstance();
            switch (config.CacheType)
            {
                case ECacheType.File:
                    return FileCacheDataProvider.GetCache(key);
                case ECacheType.NetCache:
                    return NetCacheDataProvider.GetCache(key);
                default:
                    return null;
            }
        }
    }
}

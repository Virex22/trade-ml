using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Enumerator;
using System.Text.RegularExpressions;

namespace App.Provider
{
    public class CacheDataProvider
    {
        private static CacheDataProvider? _instance;

        public static CacheDataProvider getInstance()
        {
            if (_instance == null)
                _instance = new CacheDataProvider();
            return _instance;
        }

        private CacheDataProvider()
        {
            Config config = Config.GetInstance();
            if (config.Get<ECache>("cacheType") == ECache.File)
                FileCacheDataProvider.Init();
        }

        public List<Candle>? GetCache(string key)
        {
            key = this.NormalizeKey(key);
            Config config = Config.GetInstance();
            switch (config.Get<ECache>("cacheType"))
            {
                case ECache.File:
                    return FileCacheDataProvider.GetCache(key);
                case ECache.Net:
                    return NetCacheDataProvider.GetCache(key);
                default:
                    return null;
            }
        }

        public void SetCache(string key, List<Candle> candles)
        {
            key = this.NormalizeKey(key);
            Config config = Config.GetInstance();
            switch (config.Get<ECache>("cacheType"))
            {
                case ECache.File:
                    FileCacheDataProvider.SetCache(key, candles);
                    break;
                case ECache.Net:
                    NetCacheDataProvider.SetCache(key, candles);
                    break;
                default:
                    break;
            }
        }

        private string NormalizeKey(string key)
        {
            return Regex.Replace(key, "[^a-zA-Z0-9_]+", "_");
        }
    }
}

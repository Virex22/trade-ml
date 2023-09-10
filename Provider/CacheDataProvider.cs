using App.Entity;
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
            Config config = ConfigProvider.GetConfig();
            if (config.Cache.CacheType == ECache.File)
                FileCacheDataProvider.Init();
        }

        public List<Candle>? GetCache(string key)
        {
            key = this.NormalizeKey(key);
            Config config = ConfigProvider.GetConfig();
            switch (config.Cache.CacheType)
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
            Config config = ConfigProvider.GetConfig();
            switch (config.Cache.CacheType)
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

using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Provider
{
    public static class FileCacheDataProvider
    {
        public static List<Candle>? GetCache(string key)
        {
            Config config = Config.GetInstance();
            string path = config.CachePath + key;
            if (!File.Exists(path))
                return null;
            string json = File.ReadAllText(path);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Candle>>(json) ?? null;
        }
    }
}

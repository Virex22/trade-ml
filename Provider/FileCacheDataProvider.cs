using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Provider
{
    public static class FileCacheDataProvider
    {
        private const string extension = ".json";

        public static void Init()
        {
            string path = Config.GetInstance().getConfig("cachePath");
            // check if path exists
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static List<Candle>? GetCache(string key)
        {
            Config config = Config.GetInstance();
            string path = config.getConfig("cachePath") + key;
            if (!File.Exists(path))
                return null;
            string json = File.ReadAllText(path);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Candle>>(json) ?? null;
        }

        public static void SetCache(string key, List<Candle> candles)
        {
            Config config = Config.GetInstance();
            string path = config.getConfig("cachePath") + key + extension;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(candles);
            File.WriteAllText(path, json);
        }
    }
}

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
            string path = Config.GetInstance().GetConfig("cachePath");
            // check if path exists
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static List<Candle>? GetCache(string key)
        {
            Config config = Config.GetInstance();
            string path = config.GetConfig("cachePath") + key + extension;
            if (!File.Exists(path))
                return null;
            string json = File.ReadAllText(path);
            List<Candle>? result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Candle>>(json) ?? null;
            if (result != null)
                Console.WriteLine("Successfully loaded cache for " + key);
            else
                Console.WriteLine("Failed to load cache for " + key);
            return result;
        }

        public static void SetCache(string key, List<Candle> candles)
        {
            Config config = Config.GetInstance();
            string path = config.GetConfig("cachePath") + key + extension;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(candles);
            File.WriteAllText(path, json);
        }
    }
}

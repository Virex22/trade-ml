using App.Entity;

namespace App.Provider
{
    public static class FileCacheDataProvider
    {
        private const string extension = ".json";

        public static void Init()
        {
            string path = ConfigProvider.GetConfig().Cache.CachePath;
            Directory.CreateDirectory(path);
        }

        public static List<Candle>? GetCache(string key)
        {
            string cachePath = ConfigProvider.GetConfig().Cache.CachePath;
            string filePath = Path.Combine(cachePath, key + extension);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Cache not found for key: " + key);
                return null;
            }

            string json = File.ReadAllText(filePath);
            List<Candle>? result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Candle>>(json);

            if (result != null)
                Console.WriteLine("Successfully loaded cache for key: " + key);
            else
                Console.WriteLine("Failed to load cache for key: " + key);

            return result;
        }

        public static void SetCache(string key, List<Candle> candles)
        {
            string path = ConfigProvider.GetConfig().Cache.CachePath + key + extension;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(candles);
            File.WriteAllText(path, json);
        }
    }
}

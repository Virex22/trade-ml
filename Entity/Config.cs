using App.Enumerator;
using Newtonsoft.Json;
using System;


namespace App.Entity
{
    public class Config
    {
        public string Interval { get; set; }
        public string Symbol { get; set; }
        public ECacheType CacheType { get; set; }
        public string CachePath { get; set; }
        

        private static Config? _instance = null;

        private Config()
        {
            // load config from file 
            if (!File.Exists(@"Config.json"))
                throw new Exception("Config file not found");
            string json = File.ReadAllText(@"Config.json");
            dynamic? config = JsonConvert.DeserializeObject<dynamic>(json);
            if (config == null)
                throw new Exception("Config file is empty");
            this.Interval = config.interval;
            try
            {
                string stringCacheType = config.cacheType ?? "None";
                this.CacheType = Enum.Parse<ECacheType>(stringCacheType);
            }
            catch (Exception)
            {
                throw new Exception("cacheType is not valid");
            }
            this.CachePath = config.cachePath;
            this.Symbol = config.symbol;
        }

        public static Config GetInstance()
        {
            if (_instance == null)
                _instance = new Config();
            return _instance;
        }

        public void debugConfig()
        {
            Console.WriteLine("Config:");
            Console.WriteLine("Interval: " + this.Interval);
            Console.WriteLine("CacheType: " + this.CacheType);
            Console.WriteLine("CachePath: " + this.CachePath);
            Console.WriteLine("Symbol: " + this.Symbol);
        }
    }
}

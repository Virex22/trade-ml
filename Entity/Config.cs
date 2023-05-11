using App.Enumerator;
using Newtonsoft.Json;
using System;


namespace App.Entity
{
    public class Config
    {
        private dynamic config;
        

        private static Config? _instance = null;

        private Config()
        {
            // load config from file 
            if (!File.Exists(@"Config.json"))
                throw new System.Exception("Config file not found");
            string json = File.ReadAllText(@"Config.json");

            dynamic? config = JsonConvert.DeserializeObject<dynamic>(json);

            if (config == null)
                throw new System.Exception("Config file is empty");
            this.config = config;
            ConfigTreatment();
        }

        private void ConfigTreatment()
        {
            try
            {
                string stringCacheType = config.cacheType ?? "None";
                config.cacheType = Enum.Parse<ECache>(stringCacheType);
            }
            catch (System.Exception)
            {
                throw new System.Exception("cacheType is not valid");
            }
        }

        public static Config GetInstance()
        {
            if (_instance == null)
                _instance = new Config();
            return _instance;
        }

        public void DebugConfig()
        {
            foreach (var item in config)
            {
                Console.WriteLine(item);
            }
        }

        public dynamic GetConfig (string key)
        {
            return config[key];
        }
    }
}

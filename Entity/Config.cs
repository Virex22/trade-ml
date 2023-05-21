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
            LoadConfigFromFile();
            ConfigTreatment();
        }

        private void LoadConfigFromFile()
        {
            string configFilePath = @"Config.json";

            if (!File.Exists(configFilePath))
                throw new System.Exception("Config file not found");

            string json = File.ReadAllText(configFilePath);
            this.config = JsonConvert.DeserializeObject<dynamic>(json) ?? throw new System.Exception("Config file is empty");
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

        public void Debug()
        {
            foreach (var item in config)
                Console.WriteLine(item);
        }

        public T Get<T>(string key)
        {
            return (T)config[key];
        }
    }
}

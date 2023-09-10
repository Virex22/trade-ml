using App.Entity;
using Newtonsoft.Json;

namespace App.Provider
{
    public class ConfigProvider
    {
        private static Config? config;

        public static Config GetConfig()
        {
            if (config == null)
            {
                string configFilePath = @"Config.json";
                string json = File.ReadAllText(configFilePath);
                config = JsonConvert.DeserializeObject<Config>(json);

                if (config == null)
                    throw new ApplicationException("Config file is empty");
            }
            return config;
        }

        public static void Debug()
        {
            Config config = ConfigProvider.GetConfig();
            Console.WriteLine("Config: " + JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}


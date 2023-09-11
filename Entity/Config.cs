using App.Enumerator;


namespace App.Entity
{
    public class Config
    {
        public string Symbol { get; set; } = "";
        public string Interval { get; set; } = "";
        public CacheConfig Cache { get; set; } = new CacheConfig();
        public ConsoleMessageConfig consoleMessage { get; set; } = new ConsoleMessageConfig();
        public string ReportPath { get; set; } = "";
        public int TestIntervalDay { get; set; } = 0;
        public int VariationAmplitudeCoef { get; set; } = 0;
        public decimal InitialAmount { get; set; } = 0.0m;
        public decimal BasedTradeAmountPercentage { get; set; } = 0.0m;
        public decimal PlateformFeePercentage { get; set; } = 0.0m;
        public int HistMinDataBufferLen { get; set; } = 0;
        public bool BaseTradeOnLostPercentage { get; set; } = false;
        public decimal LostPercentage { get; set; } = 0.0m;
    }

    public class CacheConfig
    {
        public ECache CacheType { get; set; } = ECache.None;
        public string CachePath { get; set; } = "";
    }

    public class ConsoleMessageConfig
    {
        public bool cache { get; set; } = true;
        public bool tradeResult { get; set; } = true;
        public bool executionTime { get; set; } = true;
        public bool config { get; set; } = true;
        public bool reportPath { get; set; } = true;
    }
}

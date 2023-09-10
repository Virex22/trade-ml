using App.Core.Parameters;
using App.Entity;
using App.Provider;

namespace App.Core.Reporting
{
    public class SimulationReportData
    {
        public SimulationReportData()
        {
            PlateformFeePercentage = ConfigProvider.GetConfig().PlateformFeePercentage;
        }

        public TradingSimulationResult Result { get; set; } = new TradingSimulationResult();
        public List<Trade> Trades { get; set; } = new List<Trade>();        
        public decimal InitialBalance { get; set; }
        public StrategyParameters StrategyParameters { get; set; } = new StrategyParameters();
        public decimal PlateformFeePercentage { get; }
    }
}

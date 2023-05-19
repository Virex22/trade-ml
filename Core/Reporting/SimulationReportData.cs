using App.Core.Parameters;
using App.Entity;

namespace App.Core.Reporting
{
    public class SimulationReportData
    {
        public SimulationReportData()
        {
            PlateformFeePercentage = Config.GetInstance().Get<decimal>("plateformFeePercentage");
        }

        public TradingSimulationResult Result { get; set; }
        public List<Trade> Trades { get; set; }
        public decimal InitialBalance { get; set; }
        public StrategyParameters StrategyParameters { get; set; }
        public decimal PlateformFeePercentage { get; }
    }
}

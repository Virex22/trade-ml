using App.Core.Parameters;
using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Reporting
{
    public class SimulationReportData
    {
        public SimulationReportData()
        {
            PlateformFeePercentage = Config.GetInstance().GetConfig("plateformFeePercentage");
        }

        public TradingSimulationResult Result { get; set; }
        public List<Trade> Trades { get; set; }
        public decimal InitialBalance { get; set; }
        public StrategyParameters StrategyParameters { get; set; }
        public decimal PlateformFeePercentage { get; set; }
    }
}

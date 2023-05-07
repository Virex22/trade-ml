using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class TradingSimulationResult
    {
        public int TotalTrades { get; set; }
        public decimal TotalReturn { get; set; }
        public TimeSpan Duration { get; set; }

        public TradingSimulationResult(int totalTrades, decimal totalReturn, TimeSpan duration)
        {
            TotalTrades = totalTrades;
            TotalReturn = totalReturn;
            Duration = duration;
        }

        internal void Debug()
        {
            Console.WriteLine("TotalTrades: " + TotalTrades);
            Console.WriteLine("TotalReturn: " + TotalReturn);
            Console.WriteLine("Duration: " + Duration);
        }
    }
}

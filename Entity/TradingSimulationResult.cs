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
        public decimal TotalProfit { get; set; }
        public TimeSpan Duration { get; set; }

        internal void Debug()
        {
            string formattedDuration = string.Format("{0}d, {1}h, {2}m, {3}s, {4}ms",
                Duration.Days, Duration.Hours, Duration.Minutes, Duration.Seconds, Duration.Milliseconds);
            Console.WriteLine("TotalTrades: " + TotalTrades);
            Console.WriteLine("TotalProfit: " + TotalProfit);
            Console.WriteLine("Duration: " + formattedDuration);
        }
    }
}

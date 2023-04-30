using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Indicator
{
    public class RSIIndicator : IIndicator<decimal>
    {
        private readonly int period;
        private readonly List<decimal> closePrices;

        public RSIIndicator(int period, List<decimal> closePrices)
        {
            this.period = period;
            this.closePrices = closePrices;
        }

        public decimal Calculate()
        {
            // Implementation du calcul RSI basé sur les close prices
            decimal sumGain = 0;
            decimal sumLoss = 0;
            decimal lastPrice = closePrices[0];
            for (int i = 1; i < period; i++)
            {
                decimal priceDiff = closePrices[i] - lastPrice;
                lastPrice = closePrices[i];
                if (priceDiff >= 0)
                {
                    sumGain += priceDiff;
                }
                else
                {
                    sumLoss -= priceDiff;
                }
            }

            decimal averageGain = sumGain / period;
            decimal averageLoss = sumLoss / period;

            decimal rs = averageGain / averageLoss;

            decimal rsi = 100 - (100 / (1 + rs));

            return rsi;
        }
    }
}

using App.Entity;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Indicator
{
    public class ATRIndicator : IIndicator<decimal>
    {
        public decimal Calculate(params object[] objects)
        {
            List<Candle> candles = (List<Candle>)objects[0];
            int period = candles.Count;

            if (period < 2)
                throw new ArgumentException("List of candles must contain at least 2 elements.");

            decimal[] trueRanges = new decimal[period - 1];

            for (int i = 1; i < period; i++)
            {
                decimal trueRange = Math.Max(
                    candles[i].High - candles[i].Low,
                    Math.Max(
                        Math.Abs(candles[i].High - candles[i - 1].Close),
                        Math.Abs(candles[i].Low - candles[i - 1].Close)
                    )
                );
                trueRanges[i - 1] = trueRange;
            }

            decimal averageTrueRange = trueRanges.Sum() / (period - 1);

            return averageTrueRange;
        }
    }
}

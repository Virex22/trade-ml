﻿using App.Entity;
using App.Interface;

namespace App.Core.Indicator
{
    public class ATRIndicator : IIndicator<decimal>
    {
        public decimal ATRAverage { get; set; }
        private List<decimal> ATRLastValue = new List<decimal>();

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

            RegisterLastValue(averageTrueRange, period);

            return averageTrueRange;
        }

        private void RegisterLastValue(decimal value, int period)
        {
            ATRLastValue.Add(value);
            if (ATRLastValue.Count > period)
                ATRLastValue.RemoveAt(0);

            ATRAverage = ATRLastValue.Sum() / ATRLastValue.Count;
        }
    }
}

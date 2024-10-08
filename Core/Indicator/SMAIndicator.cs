﻿using App.Entity;
using App.Interface;

namespace App.Core.Indicator
{
    /**
     * Indicateur de moyenne mobile
     */
    public class SMAIndicator : IIndicator<decimal>
    {
        public decimal Calculate(params object[] objects)
        {
            List<Candle> candles = (List<Candle>)objects[0];
            int period = candles.Count;

            return candles.Sum(c => c.Close) / period;
        }
    }
}

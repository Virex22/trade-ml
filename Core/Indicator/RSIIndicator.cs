﻿using App.Entity;
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

        public decimal Calculate(params object[] objects)
        {
            decimal totalGain = 0;
            decimal totalLoss = 0;
            List<Candle> candles = (List<Candle>)objects[0];
            int period = candles.Count;

            if (period < 2)
            {
                throw new ArgumentException("List of candles must contain at least 2 elements.");
            }

            for (int i = 1; i < period; i++)
            {
                decimal change = candles[i].Close - candles[i - 1].Close;

                if (change > 0)
                    totalGain += change;
                else
                    totalLoss += Math.Abs(change);
            }

            decimal averageGain = totalGain / (period - 1);
            decimal averageLoss = totalLoss / (period - 1);

            decimal relativeStrength = averageLoss == 0 ? 0 : averageGain / averageLoss;

            decimal rsi = 100 - (100 / (1 + relativeStrength));

            return rsi;
        }
    }
}

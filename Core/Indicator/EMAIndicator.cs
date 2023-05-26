using App.Entity;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Indicator
{
    public class EMAIndicator : IIndicator<decimal>
    {
        public decimal Calculate(params object[] objects)
        {
            List<Candle> candles = (List<Candle>)objects[0];
            int period = candles.Count;
            decimal smoothingFactor = 2m / (period + 1);
            decimal ema = candles[0].Close;

            for (int i = 1; i < period; i++)
            {
                decimal close = candles[i].Close;
                ema = (close - ema) * smoothingFactor + ema;
            }

            return ema;
        }
    }
}

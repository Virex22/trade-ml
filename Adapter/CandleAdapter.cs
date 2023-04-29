using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Adapter
{
    public static class CandleAdapter
    {
        public static Candle AdaptBinanceApiResultToCandle(dynamic apiResult)
        {
            Candle candle = new Candle();
            candle.open_time = DateTimeOffset.FromUnixTimeMilliseconds((long)apiResult[0]);
            candle.open = apiResult[1];
            candle.high = apiResult[2];
            candle.low = apiResult[3];
            candle.close = apiResult[4];
            candle.close_time = DateTimeOffset.FromUnixTimeMilliseconds((long)apiResult[6]);
            return candle;
        }
        public static List<Candle> AdaptBinanceApiListResultToCandleList(List<dynamic> apiResults)
        {
            List<Candle> candles = new List<Candle>();
            foreach (dynamic apiResult in apiResults)
                candles.Add(AdaptBinanceApiResultToCandle(apiResult));
            return candles;
        }
    }
}

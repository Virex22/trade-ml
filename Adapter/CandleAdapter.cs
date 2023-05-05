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
            candle.OpenTime = DateTimeOffset.FromUnixTimeMilliseconds((long)apiResult[0]);
            candle.Open = apiResult[1];
            candle.High = apiResult[2];
            candle.Low = apiResult[3];
            candle.Close = apiResult[4];
            candle.CloseTime = DateTimeOffset.FromUnixTimeMilliseconds((long)apiResult[6]);
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

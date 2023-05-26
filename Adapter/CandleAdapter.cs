using App.Entity;

namespace App.Adapter
{
    public static class CandleAdapter
    {
        public static Candle AdaptBinanceApiResultToCandle(dynamic apiResult)
        {
            return new Candle
            {
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds((long)apiResult[0]),
                Open = apiResult[1],
                High = apiResult[2],
                Low = apiResult[3],
                Close = apiResult[4],
                CloseTime = DateTimeOffset.FromUnixTimeMilliseconds((long)apiResult[6])
            };
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

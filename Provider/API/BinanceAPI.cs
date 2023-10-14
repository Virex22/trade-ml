using App.Adapter;
using App.Entity;
using System;

namespace App.Provider.API
{
    public class BinanceAPI : AbstractAPI
    {
        protected override List<Candle> AdaptApiResultToCandleList(List<dynamic> data)
        {
            return CandleAdapter.AdaptBinanceApiListResultToCandleList(data);
        }

        protected override string buildUrl(DateTimeOffset startTimestamp, DateTimeOffset endTimestamp)
        {
            return this.Url
                .Replace("{symbole}", ConfigProvider.GetConfig().Symbol)
                .Replace("{interval}", ConfigProvider.GetConfig().Interval)
                .Replace("{start_timestamp}", startTimestamp.ToUnixTimeMilliseconds().ToString())
                .Replace("{end_timestamp}", endTimestamp.ToUnixTimeMilliseconds().ToString());
        }

        protected override void initUrl()
        {
            this.Url = "https://api.binance.com/api/v3/klines?symbol={symbol}&interval={interval}&startTime={start_timestamp}&endTime={end_timestamp}&limit=1000";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using App.Entity;
using Newtonsoft.Json;
using App.Adapter;
using System.Runtime.Caching;
using App.Exception;

namespace App.Provider
{
    public class TrainingDataProvider
    {
        private string _url = "https://api.binance.com/api/v3/klines?symbol={symbol}&interval={interval}&startTime={start_timestamp}&endTime={end_timestamp}&limit=1000";

        /**
         * get random day candles values from 00:00 to 23:59
         */
        public List<Candle> getRandomDayCandleValue(int maxHistoriqueDay = 365, int dayCount = 10)
        {
            List<Candle> candles = new List<Candle>();

            if (dayCount > maxHistoriqueDay)
                throw new System.Exception("dayCount must be lower than maxHistoriqueDay");

            DateTimeOffset today = DateTimeOffset.Now;
            Random random = new Random();
            DateTimeOffset start = today.AddDays(-random.Next(1 + dayCount, maxHistoriqueDay));
            DateTimeOffset end = start.AddDays(dayCount);

            for (DateTimeOffset day = start; day <= end; day = day.AddDays(1))
            {
                List<Candle> dayCandles = this.getDayCandleValue(day);
                candles.AddRange(dayCandles);
            }

            return candles;
        }

        public List<Candle> getDayCandleValue(DateTimeOffset day)
        {
            DateTimeOffset start_timestamp = new DateTimeOffset(day.Year, day.Month, day.Day, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset end_timestamp = new DateTimeOffset(day.Year, day.Month, day.Day, 23, 59, 59, TimeSpan.Zero);
            string key = string.Format("DayCandles_{0}_{1:yyyy MM dd}", Config.GetInstance().getConfig("interval"), start_timestamp);
            CacheDataProvider cacheDataProvider = CacheDataProvider.getInstance();
            List<Candle>? cache = cacheDataProvider.GetCache(key);
            if (cache != null)
                return cache;

            List<dynamic> dayData;
            string url = this.BuildUrl(start_timestamp, end_timestamp);
            string result = this.getResult(url);
            try
            {
                dayData = JsonConvert.DeserializeObject<List<dynamic>>(result) ?? new List<dynamic>();
            }
            catch (System.Exception e)
            {
                throw new ApiException(e.Message, result);
            }

            List<Candle> candles = CandleAdapter.AdaptBinanceApiListResultToCandleList(dayData);
            cacheDataProvider.SetCache(key, candles);
            return candles;
        }

        private string BuildUrl(DateTimeOffset start_timestamp, DateTimeOffset end_timestamp)
        {
            Config config = Config.GetInstance();

            return this._url.Replace("{interval}", (string)config.getConfig("interval"))
                .Replace("{symbol}", (string)config.getConfig("symbol"))
                .Replace("{start_timestamp}", start_timestamp.ToUnixTimeMilliseconds().ToString())
                .Replace("{end_timestamp}", end_timestamp.ToUnixTimeMilliseconds().ToString());
        }

        private string getResult(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}

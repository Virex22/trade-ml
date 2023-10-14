using System.Net.Http.Headers;
using App.Entity;
using Newtonsoft.Json;
using App.Adapter;
using App.Exception;
using App.Provider.API;

namespace App.Provider
{
    public class TrainingDataProvider
    {
        private AbstractAPI api;

        public TrainingDataProvider()
        {
            this.api = new BinanceAPI();
        }
        /**
         * get random day candles values from 00:00 to 23:59
         */
        public List<Candle> getRandomDayCandleValue(int maxHistoriqueDay = 365, int dayCount = 10)
        {
            if (dayCount > maxHistoriqueDay)
                throw new ArgumentException("dayCount must be lower than maxHistoriqueDay");

            DateTimeOffset today = DateTimeOffset.Now;
            Random random = new Random();
            DateTimeOffset start = today.AddDays(-random.Next(1 + dayCount, maxHistoriqueDay));
            DateTimeOffset end = start.AddDays(dayCount);

            List<Candle> candles = new List<Candle>();
            for (DateTimeOffset day = start; day <= end; day = day.AddDays(1))
            {
                candles.AddRange(GetDayCandleValues(day));
            }

            return candles;
        }

        public List<Candle> GetDayCandleValues(DateTimeOffset day)
        {
            DateTimeOffset startTimestamp = new DateTimeOffset(day.Year, day.Month, day.Day, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset endTimestamp = new DateTimeOffset(day.Year, day.Month, day.Day, 23, 59, 59, TimeSpan.Zero);
            string key = $"DayCandles_{ConfigProvider.GetConfig().Interval}_{startTimestamp:yyyy MM dd}";
            CacheDataProvider cacheDataProvider = CacheDataProvider.getInstance();
            List<Candle>? cache = cacheDataProvider.GetCache(key);
            if (cache != null)
            {
                return cache;
            }

            List<Candle> candles = api.Call(startTimestamp, endTimestamp);
            cacheDataProvider.SetCache(key, candles);
            return candles;
        }

        private string GetResult(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}

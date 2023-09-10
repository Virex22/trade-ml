using System.Net.Http.Headers;
using App.Entity;
using Newtonsoft.Json;
using App.Adapter;
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

            string url = BuildUrl(startTimestamp, endTimestamp);
            string result = GetResult(url);

            List<dynamic> dayData;
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

        private string BuildUrl(DateTimeOffset startTimestamp, DateTimeOffset endTimestamp)
        {
            Config config = ConfigProvider.GetConfig();

            return _url.Replace("{interval}", config.Interval)
                .Replace("{symbol}", config.Symbol)
                .Replace("{start_timestamp}", startTimestamp.ToUnixTimeMilliseconds().ToString())
                .Replace("{end_timestamp}", endTimestamp.ToUnixTimeMilliseconds().ToString());
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

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

namespace App.Provider
{
    public class TrainingDataProvider
    {
        private string _url = "https://api.binance.com/api/v3/klines?symbol={symbol}&interval={interval}&startTime={start_timestamp}&endTime={end_timestamp}&limit=1000";

        /**
         * get random day candles values from 00:00 to 23:59
         */
        public List<Candle> getRandomDayCandleValue(int maxHistoriqueDay = 365)
        {
            Random random = new Random();                      
            DateTimeOffset day = DateTimeOffset.Now.AddDays(Convert.ToDouble(random.Next(1, maxHistoriqueDay) * -1));
            DateTimeOffset start_timestamp = new DateTimeOffset(day.Year, day.Month, day.Day, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset end_timestamp = new DateTimeOffset(day.Year, day.Month, day.Day, 23, 59, 59, TimeSpan.Zero);

            string url = buildUrl( start_timestamp, end_timestamp);
            List<dynamic> test = JsonConvert.DeserializeObject<List<dynamic>>(this.getResult(url)) ?? new List<dynamic>();
            return CandleAdapter.AdaptBinanceApiListResultToCandleList(test);
        }

        private string buildUrl(DateTimeOffset start_timestamp, DateTimeOffset end_timestamp)
        {
            Config config = Config.GetInstance();

            return this._url.Replace("{interval}", config.Interval)
                .Replace("{symbol}", config.Symbol)
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

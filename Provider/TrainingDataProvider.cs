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

namespace App.Provider
{
    public class TrainingDataProvider
    {
        private string url = "https://api.binance.com/api/v3/klines?symbol=BTCUSDT&interval={interval}&startTime={start_timestamp}&endTime={end_timestamp}&limit=1000";

        /**
         * get yesterday candle value from 00:00 to 23:59
         */
        public List<Candle> getYesterdayCandleValue()
        {
            DateTimeOffset yesterday = DateTimeOffset.Now.AddDays(-1);
            DateTimeOffset start_timestamp = new DateTimeOffset(yesterday.Year, yesterday.Month, yesterday.Day, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset end_timestamp = new DateTimeOffset(yesterday.Year, yesterday.Month, yesterday.Day, 23, 59, 59, TimeSpan.Zero);
            string url = buildUrl("5m", start_timestamp, end_timestamp);

            List<dynamic> test = JsonConvert.DeserializeObject<List<dynamic>>(this.getResult(url)) ?? new List<dynamic>();
            return CandleAdapter.AdaptBinanceApiListResultToCandleList(test);
        }

        private string buildUrl(string interval, DateTimeOffset start_timestamp, DateTimeOffset end_timestamp)
        {
            return url.Replace("{interval}", interval)
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

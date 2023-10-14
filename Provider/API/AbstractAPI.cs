using App.Adapter;
using App.Entity;
using App.Exception;
using App.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Provider.API
{
    public abstract class AbstractAPI
    {

        public String Url { get; protected set; }

        abstract protected void initUrl();
        
        abstract protected String buildUrl(DateTimeOffset startTimestamp, DateTimeOffset endTimestamp);

        abstract protected List<Candle> AdaptApiResultToCandleList(List<dynamic>);

        public List<Candle> Call(DateTimeOffset startTimestamp, DateTimeOffset endTimestamp)
        {
            String url = this.buildUrl(startTimestamp, endTimestamp);
            String result = this.getResult(url);

            List<dynamic> data;
            try
            {
                data = JsonConvert.DeserializeObject<List<dynamic>>(result) ?? new List<dynamic>();
            }
            catch (System.Exception e)
            {
                throw new ApiException(e.Message, result);
            }

            List<Candle> candles = AdaptApiResultToCandleList(data);
            return candles;
        }

        protected String getResult(String url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}

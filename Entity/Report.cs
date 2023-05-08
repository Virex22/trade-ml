using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class Report
    {
        public enum ReportType {
            SINGLE,
            WAVES
        }
        [JsonProperty("DataType")]
        private string DataType;

        [JsonIgnore]
        ReportType Type { 
            get
            {
                return (ReportType)Enum.Parse(typeof(ReportType), DataType);
            }
            set
            {
                DataType = value.ToString();
            }
        }
        public object Data { get; set; }

        public Report(ReportType type, object data)
        {
            Type = type;
            this.Data = data;
        }
    }
}

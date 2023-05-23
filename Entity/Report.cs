using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class Report
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ReportType
        {
            SINGLE,
            WAVES
        }

        public ReportType DataType { get; set; }

        public object Data { get; set; }

        public Report(ReportType type, object data)
        {
            DataType = type;
            Data = data;
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

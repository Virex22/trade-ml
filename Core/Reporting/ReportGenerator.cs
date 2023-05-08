using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Reporting
{
    public static class ReportGenerator
    {

        public static Report GenerateSingleTestReport(DecisionMaker decisionMaker)
        {

            SimulationReportData reportData = new SimulationReportData
            {
                Result = decisionMaker.GetResults(),
                Trades = decisionMaker.GetTrades(),
                InitialBalance = decisionMaker.initialBalance
            };
            Report report = new Report(Report.ReportType.SINGLE, reportData);


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(report, Newtonsoft.Json.Formatting.Indented);

            string path = Config.GetInstance().getConfig("reportPath") + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string fileName = "report_" + DateTime.Now.ToString("HHmmss") + ".json";

            string fullPath = path + fileName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(fullPath, json);
            Console.WriteLine("Report generated at " + fullPath);

            return report;
        }
    }
}

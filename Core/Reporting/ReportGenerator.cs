using App.Core.Parameters;
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

        public static Report GenerateSingleTestReport(DecisionMaker decisionMaker, StrategyParameters strategy)
        {
            SimulationReportData reportData = new SimulationReportData
            {
                Result = decisionMaker.GetResults(),
                Trades = decisionMaker.GetTrades(),
                InitialBalance = decisionMaker.initialBalance,
                StrategyParameters = strategy
            };
            Report report = new Report(Report.ReportType.SINGLE, reportData);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(report, Newtonsoft.Json.Formatting.Indented);

            string path = Path.Combine(Config.GetInstance().Get<string>("reportPath"), DateTime.Now.ToString("yyyyMMdd"));
            string fileName = "report_" + DateTime.Now.ToString("HHmmss") + ".json";
            string fullPath = Path.Combine(path, fileName);

            Directory.CreateDirectory(path);

            File.WriteAllText(fullPath, json);
            Console.WriteLine("Report generated at " + fullPath);

            return report;
        }
    }
}

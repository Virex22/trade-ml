using App.Core.Parameters;
using App.Entity;
using App.Provider;
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
                InitialBalance = decisionMaker.InitialBalance,
                StrategyParameters = strategy
            };
            Report report = new Report(Report.ReportType.SINGLE, reportData);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(report, Newtonsoft.Json.Formatting.Indented);

            string path = Path.Combine(ConfigProvider.GetConfig().ReportPath, DateTime.Now.ToString("yyyyMMdd"));
            string fileName = "report_" + DateTime.Now.ToString("HHmmss") + ".json";
            string fullPath = Path.Combine(path, fileName);

            Directory.CreateDirectory(path);

            File.WriteAllText(fullPath, json);
            Console.WriteLine("Report generated at " + fullPath);

            return report;
        }
    }
}

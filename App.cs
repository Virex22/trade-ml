using App.Core;
using App.Core.DataSet;
using App.Core.Parameters;
using App.Core.Reporting;
using App.Entity;
using App.Provider;

namespace App
{
    public class App
    {
        public void Run()
        {
            Console.WriteLine("La configuration utilisée est la suivante :");
            ConfigProvider.Debug();

            StrategyParameters strategyParameters = StrategyParametersBuilder.BuildRandom();
            DecisionMaker trader = new DecisionMaker(strategyParameters, ConfigProvider.GetConfig().InitialAmount);
            TestingDataSet market = new TestingDataSet();

            trader.SetSubscribedDataSet(market);

            market.Load();
            market.Start();

            ReportGenerator.GenerateSingleTestReport(trader, strategyParameters);

            trader.GetResults().Debug();
        }
    }
}

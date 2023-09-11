using App.Core;
using App.Core.DataSet;
using App.Core.Event;
using App.Core.Event.LifeCycleEvents;
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
            ExecutionTimeReporter executionTimeReporter = new ExecutionTimeReporter();

            if (ConfigProvider.GetConfig().consoleMessage.config)
            {
                Console.WriteLine("La configuration utilisée est la suivante :");
                ConfigProvider.Debug();
            }

            EventBus.GetInstance().Publish(new AppInitEvent());
            StrategyParameters strategyParameters = StrategyParametersBuilder.BuildRandom();
            EventBus.GetInstance().Publish(new StrategyReadyEvent(strategyParameters));

            DecisionMaker trader = new DecisionMaker(strategyParameters, ConfigProvider.GetConfig().InitialAmount);
            EventBus.GetInstance().Publish(new DecisionMakerReadyEvent(trader));

            TestingDataSet market = new TestingDataSet();
            EventBus.GetInstance().Publish(new MarketReadyEvent(market));

            trader.SetSubscribedDataSet(market);

            market.Load();
            EventBus.GetInstance().Publish(new MarketLoadedEvent(market));

            market.Start();
            EventBus.GetInstance().Publish(new MarketProcessingDoneEvent(market));

            ReportGenerator.GenerateSingleTestReport(trader, strategyParameters);
            EventBus.GetInstance().Publish(new SimulationDoneEvent());

            if (ConfigProvider.GetConfig().consoleMessage.tradeResult)
                trader.GetResults().Debug();

            if (ConfigProvider.GetConfig().consoleMessage.executionTime)
                executionTimeReporter.Report();
        }
    }
}

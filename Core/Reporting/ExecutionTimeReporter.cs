using App.Core.DataSet;
using App.Core.Event;
using App.Core.Event.LifeCycleEvents;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Reporting
{
    public class ExecutionTimeReporter
    {
        private Dictionary<string, DateTime> _times = new Dictionary<string, DateTime>();
        private AbstractDataSet? _market;

        public ExecutionTimeReporter() { 
            EventBus.GetInstance().Subscribe<AppInitEvent>(OnAppInit);
            EventBus.GetInstance().Subscribe<StrategyReadyEvent>(OnStrategyReady);
            EventBus.GetInstance().Subscribe<DecisionMakerReadyEvent>(OnDecisionMakerReady);
            EventBus.GetInstance().Subscribe<SimulationDoneEvent>(OnSimulationDone);
            EventBus.GetInstance().Subscribe<MarketLoadedEvent>(OnMarketLoaded);
            EventBus.GetInstance().Subscribe<MarketProcessingDoneEvent>(MarketProcessingDone);
            EventBus.GetInstance().Subscribe<MarketReadyEvent>(OnMarketReady);
        }

        private void OnAppInit(AppInitEvent obj)
        {
            _times.Add("AppInitEvent", DateTime.Now);
        }

        private void OnStrategyReady(StrategyReadyEvent obj)
        {
            _times.Add("StrategyReadyEvent", DateTime.Now);
        }

        private void OnDecisionMakerReady(DecisionMakerReadyEvent obj)
        {
            _times.Add("DecisionMakerReadyEvent", DateTime.Now);
        }

        private void OnSimulationDone(SimulationDoneEvent obj)
        {
            _times.Add("SimulationDoneEvent", DateTime.Now);
        }

        private void OnMarketLoaded(MarketLoadedEvent obj)
        {
            _times.Add("MarketLoadedEvent", DateTime.Now);
            _market = obj.Market;
        }

        private void MarketProcessingDone(MarketProcessingDoneEvent obj)
        {
            _times.Add("MarketProcessingDoneEvent", DateTime.Now);
        }

        private void OnMarketReady(MarketReadyEvent obj)
        {
            _times.Add("MarketReadyEvent", DateTime.Now);
        }

        public void Report()
        {
            if (_times.ContainsKey("AppInitEvent") && _times.ContainsKey("MarketProcessingDoneEvent"))
            {
                TimeSpan totalExecutionTime = _times["MarketProcessingDoneEvent"] - _times["AppInitEvent"];
                Console.WriteLine($"Temps total d'exécution : {totalExecutionTime.TotalMilliseconds} ms");
            }

            if (_times.ContainsKey("AppInitEvent") && _times.ContainsKey("StrategyReadyEvent"))
            {
                TimeSpan strategyParameterElapsedTime = _times["StrategyReadyEvent"] - _times["AppInitEvent"];
                Console.WriteLine($"Temps pour élaborer les paramètres de la stratégie : {strategyParameterElapsedTime.TotalMilliseconds} ms");
            }

            if (_times.ContainsKey("StrategyReadyEvent") && _times.ContainsKey("DecisionMakerReadyEvent"))
            {
                TimeSpan decisionMakerElapsedTime = _times["DecisionMakerReadyEvent"] - _times["StrategyReadyEvent"];
                Console.WriteLine($"Temps pour créer le DecisionMaker : {decisionMakerElapsedTime.TotalMilliseconds} ms");
            }

            if (_times.ContainsKey("MarketLoadedEvent") && _times.ContainsKey("MarketProcessingDoneEvent") && _market != null)
            {
                TimeSpan marketLoadingElapsedTime = _times["MarketLoadedEvent"] - _times["MarketReadyEvent"];
                Console.WriteLine($"Temps pour charger le marché : {marketLoadingElapsedTime.TotalMilliseconds} ms (nombre de bougies : {_market?.Data.Count})");
            }

            if (_times.ContainsKey("MarketLoadedEvent") && _times.ContainsKey("MarketProcessingDoneEvent"))
            {
                TimeSpan marketProcessingElapsedTime = _times["MarketProcessingDoneEvent"] - _times["MarketLoadedEvent"];
                Console.WriteLine($"Temps pour traiter le marché : {marketProcessingElapsedTime.TotalMilliseconds} ms");
            }
        }
    }
}

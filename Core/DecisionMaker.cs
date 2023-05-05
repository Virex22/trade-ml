using App.Core.DataSet;
using App.Core.Parameters;
using App.Core.Predictor;
using App.Entity;
using App.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class DecisionMaker : IObserver<AbstractDataSet>
    {
        public AbstractDataSet? subscribedDataSet;

        private DecisionPredictor predictor;
        private TradeManager tradeManager;
        private GlobalParameterVariation globalParameterVariation;

        public DecisionMaker(StrategyParameters strategy)
        {
            this.predictor = DecisionPredictorBuilder.Build(strategy,this);
            this.tradeManager = new TradeManager();
            this.globalParameterVariation = (GlobalParameterVariation) strategy.GetParameterVariation("Global");
        }

        public void OnCompleted()
        {
            // No more data to process
        }

        public void OnError(System.Exception error)
        {
            // Error is not handled
        }

        public void OnNext(AbstractDataSet dataSet)
        {
            if (subscribedDataSet == null)
                subscribedDataSet = dataSet;

            List<EDecision> decisions = predictor.MakeDecision();

            double BuyRatePercentage = this.CalculateRatePercentage(decisions, EDecision.BUY);
            double SellRatePercentage = this.CalculateRatePercentage(decisions, EDecision.SELL);

            this.MakeDecision(BuyRatePercentage, SellRatePercentage);
        }

        public void SetSubscribedDataSet(AbstractDataSet dataSet)
        {
            subscribedDataSet = dataSet;
            dataSet.Subscribe(this);
        }

        public TradingSimulationResult GetResults()
        {
            int totalTrades = 0;
            decimal totalReturn = 0;
            TimeSpan duration = TimeSpan.Zero;

            TradingSimulationResult result = new TradingSimulationResult(totalTrades, totalReturn, duration);
            return result;
        }

        private void MakeDecision(double BuyRatePercentage, double SellRatePercentage)
        {

        }

        private double CalculateRatePercentage(List<EDecision> decisions, EDecision type)
        {
            if (decisions.Count == 0)
            {
                throw new ArgumentException("List of decisions cannot be empty.");
            }
            double count = decisions.Where(x => x == type).Count();
            double total = decisions.Count();
            return (count / total) * 100.0;
        }
    }
}

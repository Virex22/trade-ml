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

        public DecisionMaker(StrategyParameters strategy)
        {
            this.predictor = DecisionPredictorBuilder.Build(strategy,this);
            this.tradeManager = new TradeManager();
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

            double BuyRate = this.CalculateRate(decisions, EDecision.BUY);
            double SellRate = this.CalculateRate(decisions, EDecision.SELL);
        }

        private double CalculateRate(List<EDecision> decisions, EDecision type)
        {
            double count = decisions.Where(x => x == type).Count();
            double total = decisions.Count();
            return count / total;
        }

        public void SetSubscribedDataSet(AbstractDataSet dataSet)
        {
            subscribedDataSet = dataSet;
            dataSet.Subscribe(this);
        }

        // when is subscribe to dataset, it will call this method

        public EDecision MakeDecision()
        {
            return EDecision.HOLD;
        }

        internal TradingSimulationResult GetResults()
        {
            int totalTrades = 0;
            decimal totalReturn = 0;
            TimeSpan duration = TimeSpan.Zero;

            TradingSimulationResult result = new TradingSimulationResult(totalTrades, totalReturn, duration);
            return result;
        }
    }
}

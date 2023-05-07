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
        private Wallet wallet;

        public DecisionMaker(StrategyParameters strategy, decimal initialBalance = 1000)
        {
            this.predictor = DecisionPredictorBuilder.Build(strategy,this);
            this.wallet = new Wallet(initialBalance);
            this.globalParameterVariation = (GlobalParameterVariation)strategy.GetParameterVariation("Global");
            this.tradeManager = new TradeManager(this.wallet, globalParameterVariation);
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

            decimal BuyRatePercentage = this.CalculateRatePercentage(decisions, EDecision.BUY);
            decimal SellRatePercentage = this.CalculateRatePercentage(decisions, EDecision.SELL);

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

        private void MakeDecision(decimal BuyRatePercentage, decimal SellRatePercentage)
        {
            if (this.subscribedDataSet == null)
            {
                throw new ArgumentException("Subscribed data set cannot be null.");
            }

            if (BuyRatePercentage >= globalParameterVariation.BuyRatioToTrade)
            {
                tradeManager.OpenTrade(Trade.TradeType.Buy, this.subscribedDataSet.CurrentPrice, this.subscribedDataSet.CurrentPrice - 100, this.subscribedDataSet.CurrentPrice + 150);
            }
            else if (SellRatePercentage >= globalParameterVariation.SellRatioToTrade)
            {
                tradeManager.OpenTrade(Trade.TradeType.Sell, this.subscribedDataSet.CurrentPrice, this.subscribedDataSet.CurrentPrice + 100, this.subscribedDataSet.CurrentPrice - 150);
            }
        }

        private decimal CalculateRatePercentage(List<EDecision> decisions, EDecision type)
        {
            if (decisions.Count == 0)
            {
                throw new ArgumentException("List of decisions cannot be empty.");
            }
            decimal count = decisions.Where(x => x == type).Count();
            decimal total = decisions.Count();
            return (count / total) * 100.0m;
        }
    }
}

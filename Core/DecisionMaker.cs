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
        public AbstractDataSet? SubscribedDataSet { get; private set; }
        public decimal InitialBalance { get; private set; }

        private PredictorCollection Predictor { get; }
        private TradeManager TradeManager { get; }
        private GlobalParameterVariation GlobalParameterVariation { get; }
        private Wallet Wallet { get; }
        private DateTime StartingTime { get; set; }
        private DateTime EndTime { get; set; }


        public DecisionMaker(StrategyParameters strategy, decimal initialBalance = 1000)
        {
            Predictor = PredictorBuilder.Build(strategy, this);
            Wallet = new Wallet(initialBalance);
            GlobalParameterVariation = (GlobalParameterVariation)strategy.GetParameterVariation("Global");
            TradeManager = new TradeManager(Wallet);
            InitialBalance = initialBalance;
        }

        public void OnCompleted()
        {
            EndTime = DateTime.Now;
        }

        public void OnError(System.Exception error)
        {
            Console.WriteLine("Error: " + error.Message);
        }

        public void OnNext(AbstractDataSet dataSet)
        {
            if (SubscribedDataSet == null)
                SubscribedDataSet = dataSet;

            List<EDecision> decisions = Predictor.GetDecision();

            decimal buyRatePercentage = CalculateRatePercentage(decisions, EDecision.Buy);
            decimal sellRatePercentage = CalculateRatePercentage(decisions, EDecision.Sell);

            MakeDecision(buyRatePercentage, sellRatePercentage);
        }

        public void SetSubscribedDataSet(AbstractDataSet dataSet)
        {
            SubscribedDataSet = dataSet;
            StartingTime = DateTime.Now;
            dataSet.Subscribe(this);
            dataSet.Subscribe(TradeManager);
        }

        public TradingSimulationResult GetResults()
        {
            int totalTrades = TradeManager.GetTotalTrades();

            TradingSimulationResult result = new TradingSimulationResult()
            {
                TotalTrades = totalTrades,
                Duration = EndTime - StartingTime,
                TotalProfit = Wallet.Balance - InitialBalance
            };

            return result;
        }

        private void MakeDecision(decimal buyRatePercentage, decimal sellRatePercentage)
        {
            if (SubscribedDataSet == null)
                throw new ArgumentException("Subscribed data set cannot be null.");


            Candle currentCandle = SubscribedDataSet.Data[SubscribedDataSet.CurrentIndex];

            decimal amountToTrade = GlobalParameterVariation.TradeAmountPercentage * Wallet.Balance / 100;

            if (buyRatePercentage >= GlobalParameterVariation.BuyRatioToTrade)
            {
                TradeManager.OpenTrade(
                    Trade.ETradeType.Buy,
                    currentCandle,
                    SubscribedDataSet.CurrentPrice - 100,
                    SubscribedDataSet.CurrentPrice + 150 * GlobalParameterVariation.PayOffRatio,
                    amountToTrade
                );
            }
            else if (sellRatePercentage >= GlobalParameterVariation.SellRatioToTrade)
            {
                TradeManager.OpenTrade(
                    Trade.ETradeType.Sell,
                    currentCandle,
                    SubscribedDataSet.CurrentPrice + 100,
                    SubscribedDataSet.CurrentPrice - 150 * GlobalParameterVariation.PayOffRatio,
                    amountToTrade
                );
            }
        }

        private decimal CalculateRatePercentage(List<EDecision> decisions, EDecision type)
        {
            if (decisions.Count == 0)
                throw new ArgumentException("List of decisions cannot be empty.");

            decimal count = decisions.Count(x => x == type);
            decimal total = decisions.Count;

            return (count / total) * 100.0m;
        }

        public List<Trade> GetTrades()
        {
            IReadOnlyList<Trade> tradeList = TradeManager.ClosedTrades;
            return tradeList.ToList();
        }
    }
}

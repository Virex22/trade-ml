using App.Core.DataSet;
using App.Core.Parameters;
using App.Core.Predictor;
using App.Entity;
using App.Enumerator;
using App.Interface;
using System.Diagnostics;

namespace App.Core
{
    public class DecisionMaker : IObserver<AbstractDataSet>
    {
        public AbstractDataSet? SubscribedDataSet { get; private set; }
        public decimal InitialBalance { get; private set; }
        public decimal Balance => Wallet.Balance;

        private TradingStrategy TradingStrategy { get; }
        private TradeManager TradeManager { get; }
        private GlobalParameterVariation GlobalParameterVariation { get; }
        private Wallet Wallet { get; }
        private DateTime StartingTime { get; set; }
        private DateTime EndTime { get; set; }

        public DecisionMaker(StrategyParameters strategy, decimal initialBalance = 1000)
        {
            this.TradingStrategy = PredictorBuilder.Build(strategy, this);
            Wallet = new Wallet(initialBalance);
            GlobalParameterVariation = (GlobalParameterVariation)strategy.Get("Global");
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

            List<EDecision> decisions = TradingStrategy.GetDecisions();

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

            decimal amountToTrade = this.CalculateAmountToTrade();
            decimal lossGap = 100;

            if (buyRatePercentage >= GlobalParameterVariation.BuyRatioToTrade)
            {
                TradeManager.OpenTrade(
                    Trade.ETradeType.Buy,
                    currentCandle,
                    SubscribedDataSet.CurrentPrice - lossGap,
                    SubscribedDataSet.CurrentPrice + 150 * GlobalParameterVariation.PayOffRatio,
                    amountToTrade
                );
            }
            else if (sellRatePercentage >= GlobalParameterVariation.SellRatioToTrade)
            {
                TradeManager.OpenTrade(
                    Trade.ETradeType.Sell,
                    currentCandle,
                    SubscribedDataSet.CurrentPrice + lossGap,
                    SubscribedDataSet.CurrentPrice - 150 * GlobalParameterVariation.PayOffRatio,
                    amountToTrade
                );
            }
        }

        private decimal CalculateAmountToTrade()
        {
            bool baseTradeOnLostPercentage = Config.GetInstance().Get<bool>("baseTradeOnLostPercentage");// ex : true
            decimal lostPercentage = Config.GetInstance().Get<decimal>("lostPercentage");// ex : 0.01
            if (!baseTradeOnLostPercentage)
            {
                decimal amountToTrade = GlobalParameterVariation.TradeAmountPercentage * Wallet.Balance / 100;

                foreach (IEffect effect in TradingStrategy.GetEffects())
                    amountToTrade = effect.UseEffect(amountToTrade, this);
                return amountToTrade;
            }
            else
            {
                decimal marketLossPercentage = (SubscribedDataSet.CurrentPrice - 100 - SubscribedDataSet.CurrentPrice) / SubscribedDataSet.CurrentPrice * 100;
                decimal balanceLossAmount = Wallet.Balance * lostPercentage;
                decimal amountToTrade = balanceLossAmount / Math.Abs(marketLossPercentage) * 100;
                if (amountToTrade > Wallet.Balance)
                    amountToTrade = Wallet.Balance;
                return amountToTrade;
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

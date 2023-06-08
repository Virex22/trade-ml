using App.Core.DataSet;
using App.Entity;

namespace App.Core
{
    public class TradeManager : IObserver<AbstractDataSet>
    {
        private List<Trade> activeTrades = new List<Trade>();
        private List<Trade> closedTrades = new List<Trade>();
        private Wallet Wallet;
        private AbstractDataSet? MarketData;

        public IReadOnlyList<Trade> ActiveTrades => activeTrades.AsReadOnly();
        public IReadOnlyList<Trade> ClosedTrades => closedTrades.AsReadOnly();

        public TradeManager(Wallet wallet)
        {
            this.Wallet = wallet;
        }

        public void OpenTrade(Trade.ETradeType type, Candle candle, decimal stopLossPrice, decimal takeProfitPrice, decimal amountToTrade)
        {
            if (!IsEligible(type))
                return;

            decimal feePercent = Config.GetInstance().Get<decimal>("plateformFeePercentage");
            if (feePercent < 0)
                feePercent = 0;

            decimal feeAmount = amountToTrade / 100 * feePercent;
            if (amountToTrade + feeAmount > Wallet.Balance)
                amountToTrade = Wallet.Balance - feeAmount;

            Wallet.Withdraw(amountToTrade + feeAmount);

            Trade trade = new Trade(type, candle, stopLossPrice, takeProfitPrice, amountToTrade);

            activeTrades.Add(trade);
        }

        private bool IsEligible(Trade.ETradeType type)
        {
            if (Wallet.Balance < Wallet.InitialBalance/2) return false;
            if (type == Trade.ETradeType.Buy)
                return !activeTrades.Any(t => t.Type == Trade.ETradeType.Buy);
            else
                return !activeTrades.Any(t => t.Type == Trade.ETradeType.Sell);
        }

        public void OnNext(AbstractDataSet marketData)
        {
            this.MarketData = marketData;
            DateTimeOffset time = marketData.GetCurrentTime();

            foreach (Trade trade in activeTrades.ToList())
            {
                if (trade.HasReachedStopLoss(marketData.CurrentPrice))
                {
                    decimal tradeAmount = trade.Close(trade.StopLossPrice, time);
                    Wallet.Deposit(tradeAmount);
                    activeTrades.Remove(trade);
                    closedTrades.Add(trade);
                }
                else if (trade.HasReachedTakeProfit(marketData.CurrentPrice))
                {
                    decimal tradeAmount = trade.Close(trade.TakeProfitPrice, time);
                    Wallet.Deposit(tradeAmount);
                    activeTrades.Remove(trade);
                    closedTrades.Add(trade);
                }
            }
        }

        public void OnCompleted()
        {
            DateTimeOffset time = MarketData?.GetCurrentTime() ?? DateTimeOffset.Now;

            foreach (Trade trade in activeTrades.ToList())
            {
                decimal tradeAmount = trade.Close(trade.EntryPrice, time);
                Wallet.Deposit(tradeAmount);
            }
        }

        public void OnError(System.Exception error)
        {
            throw new NotImplementedException();
        }

        public int GetTotalTrades(bool onlyClosedTrade = true)
        {
            return onlyClosedTrade ? closedTrades.Count : activeTrades.Count + closedTrades.Count;
        }
    }
}
